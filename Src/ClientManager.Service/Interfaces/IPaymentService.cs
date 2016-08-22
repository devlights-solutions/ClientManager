using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientManager.Entities;
using ClientManager.Service.Dtos;
using Framework.Common.Mapping;

namespace ClientManager.Service.Interfaces
{
    public interface IPaymentService : IService
    {
        Task Create(Payment payment);
        Task Edit(Payment payment);
        Task Delete(int paymentId);
        IList<PaymentDto> GetAll();
        
        List<PaymentDto> GetAll(int projectId, string sortBy, string sortDirection, int pageIndex, int pageSize,
            out int pageTotal);
        Payment GetById(int id);
        int GetNextSequence(int projectId);
    }
}