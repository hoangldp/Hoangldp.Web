using System.Web.Routing;

namespace Hoangldp.Web
{
    public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes);

        int Priority { get; }
    }
}
