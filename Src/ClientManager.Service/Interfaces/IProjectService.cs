using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientManager.Entities;
using ClientManager.Service.Dtos;
using Framework.Common.Mapping;

namespace ClientManager.Service.Interfaces
{
    public interface IProjectService : IService
    {
        Task Create(Project client);
        Task Edit(Project client);
        Task Delete(int clientId);
        IList<ProjectDto> GetAll();

        List<ProjectDto> GetAll(string criteria, string sortBy, string sortDirection, int pageIndex, int pageSize,
            out int pageTotal);
        Project GetById(int id);
    }
}