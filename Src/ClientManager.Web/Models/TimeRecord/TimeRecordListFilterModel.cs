using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using ClientManager.Web.Models.List;

namespace ClientManager.Web.Models
{
    public class TimeRecordListFilterModel : FilterBaseModel
    {
        //[Display(Name = "Palabra a Buscar", Prompt = "Palabra a Buscar")]
        //public int Criteria { get; set; }
        [Required]
        [UIHint("ProjectId")]
        [Display(Name = "Project", Prompt = "Seleccionar proyecto")]
        public int? ProjectId { get; set; }

        public override RouteValueDictionary GetRouteValues(int page = 1)
        {
            var routeValues = base.GetRouteValues(page);
            routeValues.Add("ProjectId", this.ProjectId);
            return routeValues;
        }
    }
}