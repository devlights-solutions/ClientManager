using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Common.Utility;
using Framework.Data.Helpers;
using ClientManager.Data.Interfaces;
using ClientManager.Entities;
using ClientManager.Service.Dtos;
using ClientManager.Service.Interfaces;
using Framework.Common.Mapping;
using ClientManager.Data;

namespace ClientManager.Service
{
    public class DemoService : ServiceBase, IDemoService
    {
        private readonly IClock _clock;

        public DemoService(ClientManagerUow uow, IClock clock)
        {
            _clock = clock;
            Uow = uow;
        }

        public Task Create(Demo demo)
        {
            demo.CreatedDate = _clock.Now;
            Uow.Demos.Add(demo);
            return Uow.CommitAsync();
        }

        public Task Edit(Demo demo)
        {
            var currentdemo = this.GetById(demo.Id);
            //currentdemo.Name = demo.Name;

            Uow.Demos.Edit(currentdemo);
            return Uow.CommitAsync();
        }

        public Task Delete(int demoId)
        {
            var demo = GetById(demoId);
            demo.IsDeleted = true;
            Uow.Demos.Edit(demo);

            return Uow.CommitAsync();
        }

        public Demo GetById(int id)
        {
            return Uow.Demos.Get(id);
        }

        public IList<DemoDto> GetAll()
        {
            return Uow.Demos.GetAll().ProjectToList<DemoDto>();
        }

        public List<DemoDto> GetAll(string criteria, string sortBy, string sortDirection, int pageIndex, int pageSize, out int pageTotal)
        {
            var pagingCriteria = new PagingCriteria();

            pagingCriteria.PageNumber = pageIndex;
            pagingCriteria.PageSize = pageSize;
            pagingCriteria.SortBy = !string.IsNullOrEmpty(sortBy) ? sortBy : DefaultSortBy;
            pagingCriteria.SortDirection = !string.IsNullOrEmpty(sortDirection) ? sortDirection : DefaultSortDirection;

            //Expression<Func<Demo, bool>> where = x => ((string.IsNullOrEmpty(criteria) || x.Name.Contains(criteria)));

            var results = Uow.Demos.GetAll(pagingCriteria);// , where);

            pageTotal = results.PagedMetadata.TotalItemCount;

            return results.Entities.ProjectToList<DemoDto>();
        }

    }
}
