using Hoangldp.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace $RootNamespace$.App_Start
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority => 1;

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
