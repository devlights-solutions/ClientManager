using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Logging;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace Framework.Sync
{
    public class SyncManager
    {
        private readonly string _sqllocalConnectionString;
        private readonly string _sqlazureConnectionString;
        private readonly ILogger _logger;
        public static readonly string scopeName = "alltablesyncgroup";

        public SyncManager(ILogger logger)
            : this(ConfigurationManager.AppSettings["sqllocalConnectionString"],
                   ConfigurationManager.AppSettings["sqlazureConnectionString"],
                   logger)
        {
        }

        public SyncManager(string sqllocalConnectionString, string sqlazureConnectionString, ILogger logger)
        {
            _sqllocalConnectionString = sqllocalConnectionString;
            _sqlazureConnectionString = sqlazureConnectionString;
            _logger = logger;
        }

        public void Deprovision()
        {
            SqlConnection sqlServerConn = null;
            try
            {
                sqlServerConn = new SqlConnection(_sqllocalConnectionString);
                DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                SqlSyncScopeDeprovisioning sqlServerProv = new SqlSyncScopeDeprovisioning(sqlServerConn);

                sqlServerProv.DeprovisionScope(scopeName);
            }
            finally
            {
                if (sqlServerConn != null) sqlServerConn.Close();
            }
            
        }

        public void Setup(string tableNames)
        {
            if (string.IsNullOrEmpty(tableNames))
            {
                throw new ArgumentException("tableNames");
            }

            var names = tableNames.Split(',').Select(x => x.Trim());

            Setup(names);
        }

        public void Setup(IEnumerable<string> tableNames)
        {
            SqlConnection sqlServerConn = null;
            SqlConnection sqlAzureConn = null;

            try
            {
                sqlServerConn = new SqlConnection(_sqllocalConnectionString);
                sqlAzureConn = new SqlConnection(_sqlazureConnectionString);

                DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);

                foreach (var tableName in tableNames)
                {
                    DbSyncTableDescription tableDescription = SqlSyncDescriptionBuilder.GetDescriptionForTable(
                        tableName, sqlServerConn);
                    myScope.Tables.Add(tableDescription);
                }

                // Setup SQL Server for sync
                SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                if (!sqlServerProv.ScopeExists(scopeName))
                {
                    _logger.Log("Provisioning SQL Server for sync " + DateTime.Now);
                    sqlServerProv.Apply();
                    _logger.Log("Done Provisioning SQL Server for sync " + DateTime.Now);
                }
                else
                    _logger.Log("SQL Server Database server already provisioned for sync " + DateTime.Now);

                // Setup SQL Azure for sync
                SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                if (!sqlAzureProv.ScopeExists(scopeName))
                {
                    _logger.Log("Provisioning SQL Azure for sync " + DateTime.Now);
                    sqlAzureProv.Apply();
                    _logger.Log("Done Provisioning SQL Azure for sync " + DateTime.Now);
                }
                else
                    _logger.Log("SQL Azure Database server already provisioned for sync " + DateTime.Now);
            }
            finally
            {
                if (sqlAzureConn != null) sqlAzureConn.Close();
                if (sqlServerConn != null) sqlServerConn.Close();
            }
        }

        public event EventHandler<SyncProgressEventArgs> ProgressChanged;

        private void OnProgressChange(SyncProgressEventArgs syncProgressEventArgs)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, syncProgressEventArgs);
            }
        }

        public async Task Sync()
        {
            SqlConnection sqlServerConn = null;
            SqlConnection sqlAzureConn = null;

            try
            {
                sqlServerConn = new SqlConnection(_sqllocalConnectionString);
                sqlAzureConn = new SqlConnection(_sqlazureConnectionString);
                SyncOrchestrator orch = new SyncOrchestrator
                                        {
                                            LocalProvider = new SqlSyncProvider(scopeName, sqlServerConn),
                                            RemoteProvider = new SqlSyncProvider(scopeName, sqlAzureConn),
                                            Direction = SyncDirectionOrder.UploadAndDownload
                                        };

                ((SqlSyncProvider) orch.LocalProvider).ApplyChangeFailed +=
                    new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                ((SqlSyncProvider)orch.RemoteProvider).ApplyChangeFailed +=
                    new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);

                orch.SessionProgress += (sender, args) =>
                                        {
                                            OnProgressChange(new SyncProgressEventArgs((int)args.CompletedWork, (int)args.TotalWork));
                                        };

                _logger.Log(string.Format("ScopeName={0} ", scopeName.ToUpper()));
                _logger.Log(string.Format("Starting Sync " + DateTime.Now));

                var stat = await Task.Run(() => orch.Synchronize());
                
                LogStatistics(stat);
            }
            finally
            {
                if (sqlAzureConn != null) sqlAzureConn.Close();
                if (sqlServerConn != null) sqlServerConn.Close();
            }
        }

        public void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            // display conflict type
            Console.WriteLine(e.Conflict.Type);

            // display error message 
            Console.WriteLine(e.Error);
        }

        public void LogStatistics(SyncOperationStatistics syncStats)
        {
            string message;

            message = "\tSync Start Time :" + syncStats.SyncStartTime.ToString();
            _logger.Log(message);
            message = "\tSync End Time   :" + syncStats.SyncEndTime.ToString();
            _logger.Log(message);
            message = "\tUpload Changes Applied :" + syncStats.UploadChangesApplied.ToString();
            _logger.Log(message);
            message = "\tUpload Changes Failed  :" + syncStats.UploadChangesFailed.ToString();
            _logger.Log(message);
            message = "\tUpload Changes Total   :" + syncStats.UploadChangesTotal.ToString();
            _logger.Log(message);
            message = "\tDownload Changes Applied :" + syncStats.DownloadChangesApplied.ToString();
            _logger.Log(message);
            message = "\tDownload Changes Failed  :" + syncStats.DownloadChangesFailed.ToString();
            _logger.Log(message);
            message = "\tDownload Changes Total   :" + syncStats.DownloadChangesTotal.ToString();
            _logger.Log(message);
        }
    }
}
