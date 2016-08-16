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
    public class ProjectService : ServiceBase, IProjectService
    {
        private readonly IClock _clock;

        public ProjectService(IClientManagerUow uow, IClock clock)
        {
            _clock = clock;
            Uow = uow;
        }

        public Task Create(Project project)
        {
            project.CreatedDate = _clock.Now;
            Uow.Projects.Add(project);
            return Uow.CommitAsync();
        }

        public Task Edit(Project project)
        {
            var currentProject = this.GetById(project.Id);
            currentProject.Nombre = project.Nombre;
            currentProject.Descripcion = project.Descripcion;
            currentProject.FechaInicio = project.FechaInicio;
            currentProject.ClientId = project.ClientId;
            Uow.Projects.Edit(currentProject);
            return Uow.CommitAsync();
        }

        public Task Delete(int projectId)
        {
            var project = GetById(projectId);
            project.IsDeleted = true;
            Uow.Projects.Edit(project);

            return Uow.CommitAsync();
        }

        public Project GetById(int id)
        {
            return Uow.Projects.Get(id);
        }

        public IList<ProjectDto> GetAll()
        {
            return Uow.Projects.GetAll().ProjectToList<ProjectDto>();
        }

        public List<ProjectDto> GetAll(string criteria, string sortBy, string sortDirection, int pageIndex, int pageSize, out int pageTotal)
        {
            var pagingCriteria = new PagingCriteria();

            pagingCriteria.PageNumber = pageIndex;
            pagingCriteria.PageSize = pageSize;
            pagingCriteria.SortBy = !string.IsNullOrEmpty(sortBy) ? sortBy : DefaultSortBy;
            pagingCriteria.SortDirection = !string.IsNullOrEmpty(sortDirection) ? sortDirection : DefaultSortDirection;

            Expression<Func<Project, bool>> where = x => ((string.IsNullOrEmpty(criteria) || x.Nombre.Contains(criteria)));

            var results = Uow.Projects.GetAll(pagingCriteria, where);

            pageTotal = results.PagedMetadata.TotalItemCount;

            return results.Entities.ProjectToList<ProjectDto>();
        }

    }
}
