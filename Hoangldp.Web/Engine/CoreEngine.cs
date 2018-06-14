using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Hoangldp.Core.Web.Configuration;
using Hoangldp.Core.Web.Dependency;
using Hoangldp.Core.Web.Finder;

namespace Hoangldp.Core.Web.Engine
{
    public class CoreEngine : IEngine
    {
        private IContainerManager _containerManager;

        /// <summary>
        /// Container manager
        /// </summary>
        public IContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        /// <summary>
        /// Initialize components and plugins in the nop environment.
        /// </summary>
        /// <param name="config">Config</param>
        public void Initialize(CoreConfig config)
        {
            //dependencies
            var builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<CoreConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();

            ITypeFinder typeFinder = new WebAppTypeFinder(config);
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //builder = new ContainerBuilder();
            string fullNameCurrentFinder = typeFinder.GetType().FullName;
            var listTypeFinder = typeFinder.FindClassesOfType<ITypeFinder>().ToList();
            var tfInstances = new List<ITypeFinder>();
            foreach (var tfType in listTypeFinder)
            {
                var ctor = tfType.GetConstructors().FirstOrDefault(c => c.GetParameters().Length == 1);
                if (ctor != null && ctor.GetParameters()[0].ParameterType == typeof(CoreConfig))
                {
                    tfInstances.Add((ITypeFinder)Activator.CreateInstance(tfType, config));
                }
                else
                {
                    tfInstances.Add((ITypeFinder)Activator.CreateInstance(tfType));
                }
            }

            var listConfig = typeFinder.FindClassesOfType<ConfigBase>();
            foreach (var configType in listConfig)
            {
                var configBase = (ConfigBase)Activator.CreateInstance(configType);
                var instanceConfig = ConfigContext.GetConfig(configBase.SectionName);
                builder.RegisterInstance(instanceConfig).As(configType).SingleInstance();
            }

            //register dependencies provided by other assemblies
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
            {
                IDependencyRegistrar dependencyRegistrar = (IDependencyRegistrar)Activator.CreateInstance(drType);
                drInstances.Add(dependencyRegistrar);
            }
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
            {
                dependencyRegistrar.Register(builder, typeFinder, config);
            }

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            var container = builder.Build();
            _containerManager = new WebContainerManager(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
    }
}