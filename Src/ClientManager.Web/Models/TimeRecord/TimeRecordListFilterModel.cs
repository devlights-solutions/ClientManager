using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using ClientManager.Web.Models.List;

namespace ClientManager.Web.Models
{
    public class TimeRecordListFilterModel : FilterBaseModel
    {
        //[Display(Name = "Palabra a Buscar", Prompt = "Palabra a Buscar")]
        //public int Criteria { get; set; }
        [UIHint("ProjectId")]
        [Display(Name = "Project", Prompt = "Seleccionar proyecto")]
        public int? ProjectId { get; set; }

        [UIHint("UserId")]
        [Display(Name = "User", Prompt = "Seleccionar usuario")]
        public int? UserId { get; set; }

        public override RouteValueDictionary GetRouteValues(int page = 1)
        {
            var routeValues = base.GetRouteValues(page);
            routeValues.Add("ProjectId", this.ProjectId);
            routeValues.Add("UserId", this.UserId);
            return routeValues;
        }
    }
}