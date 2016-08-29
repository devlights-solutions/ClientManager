using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using ClientManager.Web.Models.List;
using System;

namespace ClientManager.Web.Models
{
    public class PaymentListFiltersModel : FilterBaseModel
    {
        //[Display(Name = "Palabra a Buscar", Prompt = "Palabra a Buscar")]
        //public int Criteria { get; set; }
        [Required]
        [UIHint("ProjectId")]
        [Display(Name = "Project", Prompt = "Seleccionar proyecto")]
        public int? ProjectId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Vencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [UIHint("Payment")]
        [Display(Name = "Pagado", Prompt = "Pagado")]
        public bool? Pagado { get; set; }

        public override RouteValueDictionary GetRouteValues(int page = 1)
        {
            var routeValues = base.GetRouteValues(page);
            routeValues.Add("ProjectId", this.ProjectId);
            routeValues.Add("Fecha Vencimiento", this.FechaVencimiento);
            routeValues.Add("Pagado", this.Pagado);

            return routeValues;
        }
    }
}