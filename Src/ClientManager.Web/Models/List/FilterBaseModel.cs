using System.Web.Routing;
using ClientManager.Service;

namespace ClientManager.Web.Models.List
{
    public abstract class FilterBaseModel : IFilter
    {
        protected FilterBaseModel()
        {
            Page = 1;
            PageSize = ServiceBase.DefaultPageSize;
            SortBy = ServiceBase.DefaultSortBy;
            SortDirection = ServiceBase.DefaultSortDirection;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SortBy { get; set; }

        public string SortDirection { get; set; }

        public virtual RouteValueDictionary GetRouteValues(int page = 1)
        {
            return new RouteValueDictionary(new
            {
                Page = page
            });
        }
    }
}