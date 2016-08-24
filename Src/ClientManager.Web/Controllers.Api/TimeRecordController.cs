using System;
using System.Threading.Tasks;
using System.Web.Http;
using ClientManager.Service.Dtos;
using ClientManager.Service.Interfaces;
using ClientManager.Web.Models;
using PagedList;

namespace ClientManager.Web.Controllers.Api
{
    //[Authorize(Roles = "TimeRecord")]
    public class TimeRecordController : BaseApiController
    {
        private ITimeRecordService _TimeRecordService;

        public TimeRecordController(ITimeRecordService timeRecordService)
        {
            _TimeRecordService = timeRecordService;
        }

        //[Authorize]
        //[OverrideAuthorization]
        public IHttpActionResult GetAll([FromUri]TimeRecordListFilterModel filters)
        {
            int pageTotal;

            var list = _TimeRecordService.GetAll(filters.ProjectId.GetValueOrDefault(), filters.SortBy, filters.SortDirection, filters.Page, filters.PageSize, out pageTotal);

            var pagedList = new StaticPagedList<TimeRecordDto>(list, filters.Page, filters.PageSize, pageTotal);

            return Ok(new
            {
                List = pagedList,
                TotalItems = pageTotal,
                PageCount = pagedList.PageCount
            });
        }

        // POST: api/TimeRecord
        public async Task<IHttpActionResult> Post(TimeRecordForm timeRecordForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var timeRecord = timeRecordForm.ToTimeRecord();

                await _TimeRecordService.Create(timeRecord);

                return Created(Request.RequestUri + timeRecord.Id.ToString(), TimeRecordDto.FromTimeRecord(timeRecord));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/TimeRecord
        public async Task<IHttpActionResult> Put(TimeRecordForm timeRecordForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeRecordExists = _TimeRecordService.GetById(timeRecordForm.Id);
            if (timeRecordExists == null)
            {
                return NotFound();
            }

            try
            {
                var timeRecord = timeRecordForm.ToTimeRecord();

                await _TimeRecordService.Edit(timeRecord);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/TimeRecord
        public async Task<IHttpActionResult> Delete(int id)
        {
            var timeRecord = _TimeRecordService.GetById(id);
            if (timeRecord == null)
            {
                return NotFound();
            }

            try
            {
                await _TimeRecordService.Delete(timeRecord.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
