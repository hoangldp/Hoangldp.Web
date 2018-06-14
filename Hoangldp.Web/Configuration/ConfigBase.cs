using System;
using System.Configuration;
using System.Xml;

namespace Hoangldp.Core.Web.Configuration
{
    /// <summary>
    /// Class config base.
    /// </summary>
    public abstract class ConfigBase : IConfigurationSectionHandler
    {
        private XmlNode _section;

        public abstract string SectionName { get; }

        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <returns>
        /// The created section handler object.
        /// </returns>
        /// <param name="parent">Parent object.</param><param name="configContext">Configuration context object.</param><param name="section">Section XML node.</param>
        public object Create(object parent, object configContext, XmlNode section)
        {
            _section = section;
            ConfigBase config = CreateConfigBase();
            return config;
        }

        private ConfigBase CreateConfigBase()
        {
            CreateConfig();
            return this;
        }

        protected abstract void CreateConfig();

        /// <summary>
        /// Get value of attribute in configuration.
        /// </summary>
        /// <param name="nodeName">Name of node.</param>
        /// <returns>The string of attribute.</returns>
        protected T GetValueAttribute<T>(string nodeName)
        {
            XmlNode node = _section.SelectSingleNode(nodeName);
            var attribute = node?.Attributes?["value"];
            if (attribute != null)
            {
                return (T)Convert.ChangeType(attribute.Value, typeof(T));
            }
            throw new NullReferenceException("Not found node or attribute.");
        }

        /// <summary>
        /// Get value of attribute in configuration.
        /// </summary>
        /// <param name="nodeName">Name of node.</param>
        /// <param name="defaultValue">Value default.</param>
        /// <returns>The string of attribute.</returns>
        protected T GetValueAttribute<T>(string nodeName, T defaultValue)
        {
            return GetValueAttribute(nodeName, "value", defaultValue);
        }

        /// <summary>
        /// Get value of attribute in configuration.
        /// </summary>
        /// <param name="nodeName">Name of node.</param>
        /// <param name="attributeName">Name of attribute.</param>
        /// <param name="defaultValue">Value default.</param>
        /// <returns>The string of attribute.</returns>
        protected T GetValueAttribute<T>(string nodeName, string attributeName, T defaultValue)
        {
            XmlNode node = _section.SelectSingleNode(nodeName);
            var attribute = node?.Attributes?[attributeName];
            if (attribute != null)
            {
                return (T)Convert.ChangeType(attribute.Value, typeof(T));
            }
            return defaultValue;
        }
    }
}