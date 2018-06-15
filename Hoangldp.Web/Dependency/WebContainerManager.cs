using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using System.Web;

namespace Hoangldp.Web.Dependency
{
    public class WebContainerManager : ContainerManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">Conainer</param>
        public WebContainerManager(IContainer container) : base(container)
        {
        }

        /// <summary>
        /// Get current scope.
        /// </summary>
        /// <returns>Scope.</returns>
        public override ILifetimeScope Scope()
        {
            ILifetimeScope scope = null;
            try
            {
                if (HttpContext.Current != null)
                {
                    scope = AutofacDependencyResolver.Current.RequestLifetimeScope;
                    //scope = GlobalConfiguration.Configuration.DependencyResolver.GetRequestLifetimeScope();
                }

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                if (scope == null)
                {
                    scope = Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
                }
            }
            catch (System.Exception)
            {
                //we can get an exception here if RequestLifetimeScope is already disposed
                //for example, requested in or after "Application_EndRequest" handler
                //but note that usually it should never happen

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                scope = Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            return scope;
        }
    }
}
