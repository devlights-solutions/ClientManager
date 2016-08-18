using System;
using System.Web.Mvc;
using AutoMapper;
using Framework.Common.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ClientManager.Web.Models
{
    public class PaymentForm : IMapFrom<Entities.Payment>
    {
        [HiddenInput]
        public int? Id { get; set; }
        [ReadOnly(true)]
        [Required]
        public int Secuencia { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [Required]        
        public bool Pagado { get; set; }  
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Pago")]      
        public DateTime? FechaPago { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Vencimiento")]
        public DateTime? FechaVencimiento { get; set; }        
        [Required]
        //[UIHint("ProjectId")]
        //[Display(Name = "Project")]
        [HiddenInput]
        public int? ProjectId { get; set; }
            
        public Entities.Payment ToPayment()
        {
            var payment = Mapper.Map<PaymentForm, Entities.Payment>(this);
            return payment;
        }

        public static PaymentForm FromPayment(Entities.Payment payment)
        {
            var form = Mapper.Map<Entities.Payment, PaymentForm>(payment);
            return form;
        }
    }
}
