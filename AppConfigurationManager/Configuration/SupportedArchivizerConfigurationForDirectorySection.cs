using AppConfigurationManager.Data;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AppConfigurationManager.Configuration
{
    internal class SupportedArchivizerConfigurationForDirectorySection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ArchivizerConfigurationForDirectoryCollection ConfigurationForDirectory => (ArchivizerConfigurationForDirectoryCollection)base[""];

        internal IDictionary<String, ArchivizerConfigurationForDirectory> GetConfiguration()
        {
            var readedConfiguration = new Dictionary<String, ArchivizerConfigurationForDirectory>();

            for (int i = 0; i < ConfigurationForDirectory.Count; i++)
            {
                var confItem = ConfigurationForDirectory[i];
                var preparedConfItem = new ArchivizerConfigurationForDirectory(confItem);
                readedConfiguration.Add(preparedConfItem.DirectoryFullName, preparedConfItem);
            }
            return readedConfiguration;
        }
    }
}