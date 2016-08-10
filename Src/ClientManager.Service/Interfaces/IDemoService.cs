using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientManager.Entities;
using ClientManager.Service.Dtos;
using Framework.Common.Mapping;

namespace ClientManager.Service.Interfaces
{
    public interface IDemoService : IService
    {
        Task Create(Demo demo);
        Task Edit(Demo demo);
        Task Delete(Guid demoId);
        IList<DemoDto> GetAll();

        List<DemoDto> GetAll(string criteria, string sortBy, string sortDirection, int pageIndex, int pageSize,
            out int pageTotal);
        Demo GetById(Guid id);
    }
}