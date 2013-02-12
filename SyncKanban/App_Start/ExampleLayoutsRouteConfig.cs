using System.Web.Routing;
using SyncKanban.Controllers;
using NavigationRoutes;

namespace BootstrapMvcSample
{
    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapNavigationRoute<HomeController>("Boards", c => c.Index());
        }
    }
}