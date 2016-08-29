using System;
using System.Threading.Tasks;
using System.Web.Http;
using ClientManager.Service.Dtos;
using ClientManager.Service.Interfaces;
using ClientManager.Web.Models;
using PagedList;
using ClientManager.Entities;
using ClientManager.Service.Models;

namespace ClientManager.Web.Controllers.Api
{
    //[Authorize(Roles = "Project")]
    public class ProjectController : BaseApiController
    {
        private IProjectService _ProjectService;
        private IPaymentService _PaymentService;

        public ProjectController(IProjectService projectService, IPaymentService paymentService)
        {
            _ProjectService = projectService;
            _PaymentService = paymentService;
        }

        //[Authorize]
        //[OverrideAuthorization]
        public IHttpActionResult GetAll([FromUri]ProjectListFiltersModel filters)
        {
            int pageTotal;

            var list = _ProjectService.GetAll(filters.Criteria, filters.ClientId, filters.SortBy, filters.SortDirection, filters.Page, filters.PageSize, out pageTotal);

            var pagedList = new StaticPagedList<ProjectDto>(list, filters.Page, filters.PageSize, pageTotal);

            return Ok(new
            {
                List = pagedList,
                TotalItems = pageTotal,
                PageCount = pagedList.PageCount
            });
        }

        // POST: api/Project
        public async Task<IHttpActionResult> Post(ProjectForm projectForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                

               var project = await _ProjectService.Create(projectForm);

                return Created(Request.RequestUri + project.Id.ToString(), ProjectDto.FromProject(project));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Project
        public async Task<IHttpActionResult> Put(ProjectForm projectForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectExists = _ProjectService.GetById(projectForm.Id.GetValueOrDefault());
            if (projectExists == null)
            {
                return NotFound();
            }

            try
            {
                var project = projectForm.ToProject();

                await _ProjectService.Edit(project);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Project
        public async Task<IHttpActionResult> Delete(int id)
        {
            var project = _ProjectService.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            try
            {
                await _ProjectService.Delete(project.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
