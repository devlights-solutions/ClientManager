using ClientManager.Service.Interfaces;
using ClientManager.Service.Models;
using ClientManager.Web.Models;
using System.Web.Mvc;

namespace ClientManager.Web.Controllers
{

    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController()
        {
        }
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(ProjectListFiltersModel filters)
        {
            return View(filters);
        }

        public ActionResult Detail(int id)
        {
            var project = _projectService.GetById(id);
            var projectForm = ProjectForm.FromProject(project);
            return PartialView(projectForm);
        }

        public ActionResult Create()
        {
            var projectForm = new ProjectForm();
            return PartialView(projectForm);
        }


        public ActionResult edit(int id)
        {
            var project = _projectService.GetById(id);
            var projectForm = ProjectForm.FromProject(project);
            return PartialView(projectForm);
        }
    }
}