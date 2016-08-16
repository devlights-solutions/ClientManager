using System;
using System.Web.Mvc;
using ClientManager.Service.Interfaces;
using ClientManager.Web.Models;

namespace ClientManager.Web.Controllers
{
    
    public class DemoController : BaseController
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        public ActionResult Index(DemoListFiltersModel filters)
        {
            return View(filters);
        }

        public ActionResult Detail(int id)
        {
            var demo = _demoService.GetById(id);
            var demoForm = DemoForm.FromDemo(demo);
            return PartialView(demoForm);
        }

        public ActionResult Create()
        {
            var demoForm = new DemoForm();
            return PartialView(demoForm);
        }


        public ActionResult Edit(int id)
        {
            var demo = _demoService.GetById(id);
            var demoForm = DemoForm.FromDemo(demo);
            return PartialView(demoForm);
        }
    }
}