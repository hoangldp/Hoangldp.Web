using Autofac;
using Hoangldp.Web.Configuration;
using Hoangldp.Web.Finder;

namespace Hoangldp.Web.Dependency
{
    /// <summary>
    /// Define action to register dependency.
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces.
        /// </summary>
        /// <param name="builder">Container builder.</param>
        /// <param name="typeFinder">Type finder.</param>
        /// <param name="config">Configuration of core.</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, CoreConfig config);

        /// <summary>
        /// Order of this dependency registrar implementation.
        /// </summary>
        int Order { get; }
    }
}