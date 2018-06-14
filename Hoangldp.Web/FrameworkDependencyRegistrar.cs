using Autofac;
using Hoangldp.Core.Web.Authentication;
using Hoangldp.Core.Web.Configuration;
using Hoangldp.Core.Web.Data;
using Hoangldp.Core.Web.Dependency;
using Hoangldp.Core.Web.Finder;
using Hoangldp.Web.Framework.Authentication;

namespace Hoangldp.Web.Framework
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
