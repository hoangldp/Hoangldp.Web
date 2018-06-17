using Autofac;
using Hoangldp.Web.Authentication;
using Hoangldp.Web.Caching;
using Hoangldp.Web.Configuration;
using Hoangldp.Web.Data;
using Hoangldp.Web.Dependency;
using Hoangldp.Web.Event;
using Hoangldp.Web.Finder;

namespace Hoangldp.Web
{
    public class WebDependencyRegistrar : IDependencyRegistrar
    {
        public int Order => -1;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CoreConfig config)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<PasswordHasher>().As<IPasswordHash>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuthenticatorManager<>)).As(typeof(IAuthenticatorManager<>)).InstancePerLifetimeScope();
            builder.RegisterType<SubscriptionService>().As<ISubscriptionService>().InstancePerLifetimeScope();
            builder.RegisterType<EventPublisher>().As<IEventPublisher>().InstancePerLifetimeScope();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
        }
    }
}
