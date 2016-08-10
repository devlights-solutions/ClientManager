using System;
using System.Threading.Tasks;
using System.Web.Http;
using ClientManager.Service.Dtos;
using ClientManager.Service.Interfaces;
using ClientManager.Web.Models.Demo;
using Framework.Common.Mapping;
using PagedList;

namespace ClientManager.Web.Controllers.Api
{
    //[Authorize(Roles = "Demo")]
    public class DemoController : BaseApiController
    {
        private IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        //[Authorize]
        //[OverrideAuthorization]
        public IHttpActionResult GetAll([FromUri]DemoListFiltersModel filters)
        {
            int pageTotal;

            var list = _demoService.GetAll(filters.Criteria, filters.SortBy, filters.SortDirection, filters.Page, filters.PageSize, out pageTotal);

            var pagedList = new StaticPagedList<DemoDto>(list, filters.Page, filters.PageSize, pageTotal);

            return Ok(new
            {
                List = pagedList,
                TotalItems = pageTotal,
                PageCount = pagedList.PageCount
            });
        }

        // POST: api/Demo
        public async Task<IHttpActionResult> Post(DemoForm demoForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var demo = demoForm.ToDemo();

                await _demoService.Create(demo);

                return Created(Request.RequestUri + demo.Id.ToString(), DemoDto.FromDemo(demo));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Demo
        public async Task<IHttpActionResult> Put(DemoForm demoForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var demoExists = _demoService.GetById(demoForm.Id.GetValueOrDefault());
            if (demoExists == null)
            {
                return NotFound();
            }

            try
            {
                var demo = demoForm.ToDemo();

                await _demoService.Edit(demo);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Demo
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var demo = _demoService.GetById(id);
            if (demo == null)
            {
                return NotFound();
            }

            try
            {
                await _demoService.Delete(demo.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
