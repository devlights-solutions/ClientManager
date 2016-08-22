using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClientManager.Entities;
using Framework.Common.Mapping;

namespace ClientManager.Service.Dtos
{
    public class PaymentDto : IMapFrom<Payment>
    {
        public int Id { get; set; }
        public int Secuencia { get; set; }
        public bool Pagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Monto { get; set; } 
        public string ProjectNombre { get; set; }
        public string ProjectClientRazonSocial { get; set; }



        public Payment ToPayment()
        {
            var entity = Mapper.Map<PaymentDto, Payment>(this);
            return entity;
        }

        public static PaymentDto FromPayment(Payment entity)
        {
            var form = Mapper.Map<Payment, PaymentDto>(entity);
            return form;
        }
    }
}
