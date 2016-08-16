using System;
using ClientManager.Service.Interfaces;
using Framework.Data.Repository;
using ClientManager.Data.Interfaces;

namespace ClientManager.Service
{
    public class ServiceBase : IService
    {
        public const string DefaultSortBy = "CreatedDate";
        public const string DefaultSortDirection = "DESC";
        public const int DefaultPageSize = 50;

        protected IClientManagerUow Uow { get; set; }

        protected IUowFactory UowFactory { get; set; }

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
                if (Uow != null)
                {
                    Uow.Dispose();
                    Uow = null;
                }
            }
        }

        #endregion
    }
}
