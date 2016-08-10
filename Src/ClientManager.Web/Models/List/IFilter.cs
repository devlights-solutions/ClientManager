using System.Web.Routing;

namespace ClientManager.Web.Models.List
{
    public interface IFilter
    {
        RouteValueDictionary GetRouteValues(int page = 1);
    }
}
