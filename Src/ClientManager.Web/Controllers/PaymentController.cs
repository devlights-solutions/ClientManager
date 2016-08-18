using ClientManager.Service.Interfaces;
using ClientManager.Web.Models;
using System.Web.Mvc;

namespace ClientManager.Web.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController()
        {
        }
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(PaymentListFiltersModel filters)
        {
            return View(filters);
        }

        public ActionResult Detail(int id)
        {
            var payment = _paymentService.GetById(id);
            var paymentForm = PaymentForm.FromPayment(payment);
            return PartialView(paymentForm);
        }

        public ActionResult Create(int projectId)
        {
            var paymentForm = new PaymentForm();
            paymentForm.Secuencia = _paymentService.GetNextSequence(projectId);
            //tu vieja
            paymentForm.ProjectId = projectId;            
            return PartialView(paymentForm);
        }


        public ActionResult edit(int id)
        {
            var payment = _paymentService.GetById(id);
            var paymentForm = PaymentForm.FromPayment(payment);
            return PartialView(paymentForm);
        }
    }
}