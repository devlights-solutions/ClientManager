using System;
using System.Threading.Tasks;
using System.Web.Http;
using ClientManager.Service.Dtos;
using ClientManager.Service.Interfaces;
using ClientManager.Web.Models;
using PagedList;

namespace ClientManager.Web.Controllers.Api
{
    //[Authorize(Roles = "Payment")]
    public class PaymentController : BaseApiController
    {
        private IPaymentService _PaymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _PaymentService = paymentService;
        }

        //[Authorize]
        //[OverrideAuthorization]
        public IHttpActionResult GetAll([FromUri]PaymentListFiltersModel filters)
        {
            int pageTotal;

            var list = _PaymentService.GetAll(filters.ProjectId.GetValueOrDefault(), filters.FechaVencimiento, filters.Pagado, filters.SortBy, filters.SortDirection, filters.Page, filters.PageSize, out pageTotal);

            var pagedList = new StaticPagedList<PaymentDto>(list, filters.Page, filters.PageSize, pageTotal);

            return Ok(new
            {
                List = pagedList,
                TotalItems = pageTotal,
                PageCount = pagedList.PageCount
            });
        }

        // POST: api/Payment
        public async Task<IHttpActionResult> Post(PaymentForm paymentForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var payment = paymentForm.ToPayment();

                await _PaymentService.Create(payment);

                return Created(Request.RequestUri + payment.Id.ToString(), PaymentDto.FromPayment(payment));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Payment
        public async Task<IHttpActionResult> Put(PaymentForm paymentForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentExists = _PaymentService.GetById(paymentForm.Id);
            if (paymentExists == null)
            {
                return NotFound();
            }

            try
            {
                var payment = paymentForm.ToPayment();

                await _PaymentService.Edit(payment);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Payment
        public async Task<IHttpActionResult> Delete(int id)
        {
            var payment = _PaymentService.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }

            try
            {
                await _PaymentService.Delete(payment.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
