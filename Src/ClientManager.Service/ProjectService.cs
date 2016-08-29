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
using ClientManager.Service.Models;

namespace ClientManager.Service
{
    public class ProjectService : ServiceBase, IProjectService
    {
        private readonly IClock _clock;
        private readonly IPaymentService _paymentService;

        public ProjectService(IClientManagerUow uow, IClock clock,IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _clock = clock;
            Uow = uow;
        }

        public async Task<Project> Create(ProjectForm projectForm)
        {
            var project = projectForm.ToProject();
            project.CreatedDate = _clock.Now;
            Uow.Projects.Add(project);
                        
            if (projectForm.Cuotas)
            {
                var costoCuota = project.CostoTotal.GetValueOrDefault() / projectForm.CantidadCuotas.GetValueOrDefault();
                for (int i = 0; i < projectForm.CantidadCuotas; i++)
                {
                    var pago = new Payment();
                    pago.Secuencia = i;
                    pago.Monto = costoCuota;
                    pago.Pagado = false;
                    pago.FechaPago = projectForm.FechaPago.GetValueOrDefault().AddMonths(i);
                    pago.FechaVencimiento = projectForm.FechaPago.GetValueOrDefault().AddMonths(i + 1);
                    pago.ProjectId = project.Id;
                    Uow.Payments.Add(pago);

                }
            }

            await Uow.CommitAsync();

            return project;
        }

        public Task Edit(Project project)
        {
            var currentProject = this.GetById(project.Id);
            currentProject.Nombre = project.Nombre;
            currentProject.Descripcion = project.Descripcion;
            currentProject.FechaInicio = project.FechaInicio;
            currentProject.CostoTotal = project.CostoTotal;
            currentProject.ClientId = project.ClientId;
            Uow.Projects.Edit(currentProject);
            return Uow.CommitAsync();
        }

        public Task Delete(int projectId)
        {
            var project = GetById(projectId);

            foreach (var payment in project.Payments)
            {
                Uow.Payments.Delete(payment.Id);
            }            
            Uow.Projects.Delete(project);
            return Uow.CommitAsync();
        }

        public Project GetById(int id)
        {

            return Uow.Projects.Get(id, x => x.Payments);
        }

        public IList<ProjectDto> GetAll()
        {
            return Uow.Projects.GetAll().ProjectToList<ProjectDto>();
        }

        public List<ProjectDto> GetAll(string criteria, int? clientId, string sortBy, string sortDirection, int pageIndex, int pageSize, out int pageTotal)
        {
            var pagingCriteria = new PagingCriteria();

            pagingCriteria.PageNumber = pageIndex;
            pagingCriteria.PageSize = pageSize;
            pagingCriteria.SortBy = !string.IsNullOrEmpty(sortBy) ? sortBy : DefaultSortBy;
            pagingCriteria.SortDirection = !string.IsNullOrEmpty(sortDirection) ? sortDirection : DefaultSortDirection;

            Expression<Func<Project, bool>> where = x => ((string.IsNullOrEmpty(criteria) || x.Nombre.Contains(criteria)) && (x.ClientId == clientId || clientId == null));

            var results = Uow.Projects.GetAll(pagingCriteria, where);

            pageTotal = results.PagedMetadata.TotalItemCount;

            return results.Entities.ProjectToList<ProjectDto>();
        }

    }
}
