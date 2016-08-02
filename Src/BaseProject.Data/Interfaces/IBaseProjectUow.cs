using System.Collections.Specialized;
using BaseProject.Data;
using Framework.Data.Repository;
using BaseProject.Entities;
using PointEx.Data;

namespace BaseProject.Data.Interfaces
{
    public interface IBaseProjectUow : IUow
    {
        IRepository<Demo> Demos { get; }
        //IDemoRepository Demos { get; }

        BaseProjectDbContext DbContext { get; }
    }
}
