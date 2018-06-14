using System.Web.Routing;

namespace Hoangldp.Web.Framework
{
    public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes);

        int Priority { get; }
    }
}
