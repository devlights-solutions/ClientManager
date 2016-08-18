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

namespace ClientManager.Service
{
    public class PaymentService : ServiceBase, IPaymentService
    {
        private readonly IClock _clock;

        public PaymentService(IClientManagerUow uow, IClock clock)
        {
            _clock = clock;
            Uow = uow;
        }

        public int GetNextSequence(int projectId)
        {
            var listado = Uow.Payments.GetAll(x => x.ProjectId == projectId);
            var maxSecuencia = listado.Select(x => x.Secuencia).DefaultIfEmpty(-1).Max();
            return maxSecuencia + 1;
        }

        public Task Create(Payment payment)
        {
            payment.CreatedDate = _clock.Now;
            payment.Secuencia = GetNextSequence(payment.ProjectId);

            Uow.Payments.Add(payment);
            return Uow.CommitAsync();
        }

        public Task Edit(Payment payment)
        {
            var currentPayment = this.GetById(payment.Id);
            currentPayment.Secuencia = payment.Secuencia;
            currentPayment.Pagado = payment.Pagado;
            currentPayment.FechaPago = payment.FechaPago;
            currentPayment.Monto = payment.Monto;
            currentPayment.FechaVencimiento = payment.FechaVencimiento;
            currentPayment.ProjectId = payment.ProjectId;
            Uow.Payments.Edit(currentPayment);
            return Uow.CommitAsync();
        }

        public Task Delete(int paymentId)
        {
            var payment = GetById(paymentId);
            payment.IsDeleted = true;
            Uow.Payments.Edit(payment);

            return Uow.CommitAsync();
        }

        public Payment GetById(int id)
        {
            return Uow.Payments.Get(id);
        }

        public IList<PaymentDto> GetAll()
        {
            return Uow.Payments.GetAll().ProjectToList<PaymentDto>();
        }

        public List<PaymentDto> GetAll(int projectId, string sortBy, string sortDirection, int pageIndex, int pageSize, out int pageTotal)
        {
            var pagingCriteria = new PagingCriteria();

            pagingCriteria.PageNumber = pageIndex;
            pagingCriteria.PageSize = pageSize;
            pagingCriteria.SortBy = !string.IsNullOrEmpty(sortBy) ? sortBy : DefaultSortBy;
            pagingCriteria.SortDirection = !string.IsNullOrEmpty(sortDirection) ? sortDirection : DefaultSortDirection;

            Expression<Func<Payment, bool>> where = x => (x.ProjectId == projectId);

            var results = Uow.Payments.GetAll(pagingCriteria, where);//, where;

            pageTotal = results.PagedMetadata.TotalItemCount;

            return results.Entities.ProjectToList<PaymentDto>();
        }

    }
}
