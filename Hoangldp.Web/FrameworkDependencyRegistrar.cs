using Autofac;
using Hoangldp.Web.Authentication;
using Hoangldp.Web.Configuration;
using Hoangldp.Web.Data;
using Hoangldp.Web.Dependency;
using Hoangldp.Web.Finder;

namespace Hoangldp.Web
{
    public class FrameworkDependencyRegistrar : IDependencyRegistrar
    {
        public int Order => -1;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CoreConfig config)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerMatchingLifetimeScope();
            builder.RegisterType<PasswordHasher>().As<IPasswordHash>().InstancePerMatchingLifetimeScope();
            builder.RegisterGeneric(typeof(AuthenticatorManager<>)).As(typeof(IAuthenticatorManager<>)).InstancePerLifetimeScope();
        }
    }
}
