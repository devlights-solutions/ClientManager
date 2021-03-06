﻿using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Threading.Tasks;
using ClientManager.Data;
using Framework.Data.EntityFramework.Helpers;
using Framework.Data.Interfaces;
using Framework.Data.Repository;
using ClientManager.Data.Interfaces;
using ClientManager.Data.Repository;
using ClientManager.Entities;

namespace ClientManager.Data
{
    public class ClientManagerUow : IClientManagerUow
    {
        public ClientManagerUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();

            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        public IRepository<Demo> Demos { get { return GetStandardRepo<Demo>(); } }

        public IRepository<Client> Clients { get { return GetStandardRepo<Client>(); } }

        public IRepository<Project> Projects { get { return GetStandardRepo<Project>(); } }

        public IRepository<Payment> Payments { get { return GetStandardRepo<Payment>();  } }

        public IRepository<TimeRecord> TimeRecords { get { return GetStandardRepo<TimeRecord>(); } }




        //public IDemoRepository Demos { get { return GetRepo<IDemoRepository>(); } }

        public string ConnectionString
        {
            get
            {
                var builder = new EntityConnectionStringBuilder();
                builder.Metadata = @"res://*/ClientManagerModel.csdl|res://*/ClientManagerModel.ssdl|res://*/ClientManagerModel.msl";
                builder.Provider = "System.Data.SqlClient";
                builder.ProviderConnectionString = ConfigurationManager.ConnectionStrings["ClientManagerDbContext"].ConnectionString;
                return builder.ToString();
            }
        }

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public Task CommitAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        protected void CreateDbContext()
        {
            DbContext = new ClientManagerDbContext(ConnectionString);

            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;
            
            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        private IRepository<T> GetStandardRepo<T>() where T : class, IEntity
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        public ClientManagerDbContext DbContext { get; set; }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion


        
    }
}
