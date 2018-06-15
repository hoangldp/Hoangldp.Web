using Hoangldp.Web.Common;
using Hoangldp.Web.Configuration;
using System.Runtime.CompilerServices;

namespace Hoangldp.Web.Engine
{
    /// <summary>
    /// A static class engine of core.
    /// </summary>
    public static class EngineContext
    {
        /// <summary>
        /// Initializes a static instance of the core factory.
        /// </summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = new CoreEngine();

                var config = ConfigContext.GetConfig<CoreConfig>(new CoreConfig().SectionName);
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// Gets the singleton core engine used to access core services.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }
    }
}