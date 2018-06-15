using Hoangldp.Web.Engine;
using Hoangldp.Web.Finder;
using Hoangldp.Web.Attributes;
using Hoangldp.Web.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Hoangldp.Web
{
    public abstract class HoangldpApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
            var routeProviderTypes = typeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in routeProviderTypes)
            {
                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }
            routeProviders = routeProviders.OrderByDescending(rp => rp.Priority).ToList();
            routeProviders.ForEach(rp => rp.RegisterRoutes(routes));
        }

        protected void Application_Start()
        {
            //disable "X-AspNetMvc-Version" header name
            MvcHandler.DisableMvcResponseHeader = true;

            EngineContext.Initialize(false);

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            OnApplicationStart();
        }

        protected virtual void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            //HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
        }

        protected abstract void OnApplicationStart();
    }
}
