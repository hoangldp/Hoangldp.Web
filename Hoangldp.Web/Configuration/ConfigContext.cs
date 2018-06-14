using System.Configuration;

namespace Hoangldp.Core.Web.Configuration
{
    public static class ConfigContext
    {
        public static TConfig GetConfig<TConfig>(string nameSection) where TConfig : ConfigBase
        {
            return (TConfig) ConfigurationManager.GetSection($"base/{nameSection}");
        }

        public static ConfigBase GetConfig(string nameSection)
        {
            return (ConfigBase)ConfigurationManager.GetSection($"base/{nameSection}");
        }
    }
}