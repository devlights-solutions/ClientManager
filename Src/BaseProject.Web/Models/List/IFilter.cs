using System.Web.Routing;

namespace BaseProject.Web.Models.List
{
    public interface IFilter
    {
        RouteValueDictionary GetRouteValues(int page = 1);
    }
}
