using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using ClientManager.Data;
using Framework.Data.EntityFramework.Repository;
using Framework.Data.Helpers;
using ClientManager.Data.Interfaces;
using ClientManager.Entities;

namespace ClientManager.Data.Repository
{
    //public class DemoRepository : EFRepository<Demo>, IDemoRepository
    //{

    //    public DemoRepository(DbContext context) : base(context) { }

    //    public PagedResultList<DemoComplete> GetIndices(string criteria, PagingCriteria paging)
    //    {
    //        var paramOutPageTotal = new ObjectParameter("TotalRecords", typeof(int));
    //        var result = new PagedResultList<DemoComplete>();

    //        result.Entities = ((BaseProjectDbContext)DbContext).GetDemos(criteria,
    //                                            paging.SortBy + " " + paging.SortDirection,
    //                                            paging.PageNumber,
    //                                            paging.PageSize,
    //                                            paramOutPageTotal).ToList().AsQueryable();

    //        var totalRecords = int.Parse(paramOutPageTotal.Value.ToString());

    //        result.PagedMetadata = new PagedMetadata(totalRecords, paging.PageSize, paging.PageNumber);

    //        return result;
    //    }
    //}
}
