using System;
using Autofac;

namespace Hoangldp.Web.Dependency
{
    /// <summary>
    /// Container managerment.
    /// </summary>
    public interface IContainerManager
    {
        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class;

        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        object Resolve(Type type, ILifetimeScope scope = null);

        /// <summary>
        /// Resolve all
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved services</returns>
        T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null);

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class;

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        object ResolveUnregistered(Type type, ILifetimeScope scope = null);

        /// <summary>
        /// Try to resolve srevice
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <param name="instance">Resolved service</param>
        /// <returns>Value indicating whether service has been successfully resolved</returns>
        bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance);

        /// <summary>
        /// Check whether some service is registered (can be resolved)
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Result</returns>
        bool IsRegistered(Type serviceType, ILifetimeScope scope = null);

        /// <summary>
        /// Resolve optional
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        object ResolveOptional(Type serviceType, ILifetimeScope scope = null);

        /// <summary>
        /// Get current scope
        /// </summary>
        /// <returns>Scope</returns>
        ILifetimeScope Scope();
    }
}