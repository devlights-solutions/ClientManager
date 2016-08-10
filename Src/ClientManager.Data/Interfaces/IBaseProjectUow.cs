using System.Collections.Specialized;
using ClientManager.Data;
using Framework.Data.Repository;
using ClientManager.Entities;
using PointEx.Data;

namespace ClientManager.Data.Interfaces
{
    public interface IBaseProjectUow : IUow
    {
        IRepository<Demo> Demos { get; }
        //IDemoRepository Demos { get; }

        BaseProjectDbContext DbContext { get; }
    }
}
