using Autofac;
using Hoangldp.Web.Configuration;
using Hoangldp.Web.Dependency;
using Hoangldp.Web.Finder;

namespace $RootNamespace$.App_Start
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CoreConfig config)
        {
            //builder.RegisterType<ExampleContext>().As<IDataContext>().InstancePerLifetimeScope();
            //builder.RegisterType<ExampleUserService>().As<IUserLoginService<ModelLogin>>().InstancePerLifetimeScope();
        }
    }
}
