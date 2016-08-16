using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using ClientManager.Web.Models.List;

namespace ClientManager.Web.Models
{
    public class ProjectListFiltersModel : FilterBaseModel
    {
        [Display(Name = "Palabra a Buscar", Prompt = "Palabra a Buscar")]
        public string Criteria { get; set; }

        public override RouteValueDictionary GetRouteValues(int page = 1)
        {
            var routeValues = base.GetRouteValues(page);
            routeValues.Add("Criteria", this.Criteria);
            return routeValues;
        }
    }
}