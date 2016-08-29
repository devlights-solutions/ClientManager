using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientManager.Entities;
using ClientManager.Service.Dtos;
using Framework.Common.Mapping;
using ClientManager.Service.Models;

namespace ClientManager.Service.Interfaces
{
    public interface IProjectService : IService
    {
        Task<Project> Create(ProjectForm projectForm);
        Task Edit(Project project);
        Task Delete(int projectId);
        IList<ProjectDto> GetAll();

        List<ProjectDto> GetAll(string criteria, int? clientId, string sortBy, string sortDirection, int pageIndex, int pageSize,
            out int pageTotal);
        Project GetById(int id);
    }
}