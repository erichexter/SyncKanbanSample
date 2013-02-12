using System.Web.Routing;

namespace NavigationRoutes
{
    public class NavigationRouteFilter : INavigationRouteFilter
    {
        public bool ShouldRemove(Route route)
        {
            return true;
        }
    }
}