using System.Collections.Specialized;
using ClientManager.Data;
using Framework.Data.Repository;
using ClientManager.Entities;
using PointEx.Data;

namespace ClientManager.Data.Interfaces
{
    public interface IClientManagerUow : IUow
    {
        IRepository<Demo> Demos { get; }
        //IDemoRepository Demos { get; }
        IRepository<Client> Clients { get; }
        IRepository<Project> Projects { get; }

        ClientManagerDbContext DbContext { get; }
    }
}
