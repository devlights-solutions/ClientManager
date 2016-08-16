using ClientManager.Service.Interfaces;
using ClientManager.Web.Models;
using System.Web.Mvc;

namespace ClientManager.Web.Controllers
{

    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientController()
        {
        }
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(ClientListFiltersModel filters)
        {
            return View(filters);
        }

        public ActionResult Detail(int id)
        {
            var client = _clientService.GetById(id);
            var clientForm = ClientForm.FromClient(client);
            return PartialView(clientForm);
        }

        public ActionResult Create()
        {
            var clientForm = new ClientForm();
            return PartialView(clientForm);
        }


        public ActionResult edit(int id)
        {
            var client = _clientService.GetById(id);
            var clientForm = ClientForm.FromClient(client);
            return PartialView(clientForm);
        }
    }
}