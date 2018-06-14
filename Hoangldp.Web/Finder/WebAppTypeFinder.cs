using Hoangldp.Core.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Hoangldp.Core.Web.Finder
{
    /// <summary>
    /// Provides information about types in the current web application.
    /// Optionally this class can look at all assemblies in the bin folder.
    /// </summary>
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        private bool _binFolderAssembliesLoaded;

        /// <summary>
        /// Gets or sets wether assemblies in the bin folder of the web application should be specificly checked for beeing loaded on application load. This is need in situations where plugins need to be loaded in the AppDomain after the application been reloaded.
        /// </summary>
        private bool EnsureBinFolderAssembliesLoaded { get; set; } = true;

        /// <summary>
        /// Gets or sets the pattern for dll that will be investigated. For ease of use this defaults to match all but to increase performance you might want to configure a pattern that includes assemblies and your own.
        /// </summary>
        /// <remarks>
        /// If you change this so that Nop assemblies arn't investigated (e.g. by not including something like "^Nop|..." you may break core functionality.
        /// </remarks>
        public sealed override string AssemblyRestrictToLoadingPattern { get; set; }

        public WebAppTypeFinder(CoreConfig coreConfig)
        {
            string pattern = "^Hoangldp";
            if (!string.IsNullOrEmpty(coreConfig.LibLoadingPattern))
            {
                pattern += "|" + coreConfig.LibLoadingPattern;
            }
            AssemblyRestrictToLoadingPattern = pattern;
        }

        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HttpRuntime.BinDirectory;
            }

            //not hosted. For example, run either in unit tests
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (EnsureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                //binPath = _webHelper.MapPath("~/bin");
                LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }
    }
}
