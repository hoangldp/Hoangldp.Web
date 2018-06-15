using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hoangldp.Web.Finder
{
    /// <summary>
    /// Find type in application.
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// Gets the assemblies related to the current implementation.
        /// </summary>
        /// <returns>A list of assemblies that should be loaded by the factory.</returns>
        IList<Assembly> GetAssemblies();

        /// <summary>
        /// Find all class assign from a type.
        /// </summary>
        /// <param name="assignTypeFrom">Type assign.</param>
        /// <param name="onlyConcreteClasses">Only is class, no abstract.</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        /// <summary>
        /// Find all class assign from a type in a list assemblies.
        /// </summary>
        /// <param name="assignTypeFrom">Type assign.</param>
        /// <param name="assemblies">The list assemblies.</param>
        /// <param name="onlyConcreteClasses">Only is class, no abstract.</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        /// <summary>
        /// Find all class assign from a type.
        /// </summary>
        /// <typeparam name="T">Type assign.</typeparam>
        /// <param name="onlyConcreteClasses">Only is class, no abstract.</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        /// <summary>
        /// Find all class assign from a type in a list assemblies.
        /// </summary>
        /// <typeparam name="T">Type assign.</typeparam>
        /// <param name="assemblies">The list assemblies.</param>
        /// <param name="onlyConcreteClasses">Only is class, no abstract.</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}