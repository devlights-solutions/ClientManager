using ClientManager.Service.Interfaces;
using ClientManager.Web.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ClientManager.Web.Controllers
{

    public class TimeRecordController : BaseController
    {
        private readonly ITimeRecordService _timeRecordService;

        public TimeRecordController()
        {
        }
        public TimeRecordController(ITimeRecordService timeRecordService)
        {
            _timeRecordService = timeRecordService;
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(TimeRecordListFilterModel filters)
        {
            return View(filters);
        }

        public ActionResult Detail(int id)
        {
            var timeRecord = _timeRecordService.GetById(id);
            var timeRecordForm = TimeRecordForm.FromTimeRecord(timeRecord);
            return PartialView(timeRecordForm);
        }

        public ActionResult Create(int projectId, int? userId)
        {
            var timeRecordForm = new TimeRecordForm();
            timeRecordForm.ProjectId = projectId;
            var usuario = userId;

            if (User.IsInRole("Developer"))
            {
                usuario = User.Identity.GetUserId<int>();
            }
            timeRecordForm.UserId = usuario;
            return PartialView(timeRecordForm);
        }


        public ActionResult Edit(int id)
        {
            var timeRecord = _timeRecordService.GetById(id);
            var timeRecordForm = TimeRecordForm.FromTimeRecord(timeRecord);
            return PartialView(timeRecordForm);
        }
    }
}