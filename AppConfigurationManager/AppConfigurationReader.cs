using SystemConfigurationManager = System.Configuration.ConfigurationManager;
using System.Configuration;
using AppConfigurationManager.Configuration;
using AppConfigurationManager.Data;
using System.Collections.ObjectModel;
using System.IO;

namespace AppConfigurationManager
{
    public class AppConfigurationReader
    {
        public ArchivizerConfiguration ReadConfiguration()
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap() { ExeConfigFilename = "ArchivizerConfiguration.config" };
            var config = SystemConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            var globalConfiguration = config.GetSection("GlobalConfigurationSection") as GlobalConfigurationSection;
            var supportedDirectory = config.GetSection("SupportedArchivizerConfigurationForDirectorySection") as SupportedArchivizerConfigurationForDirectorySection;

            var configurations = supportedDirectory.GetConfiguration();

            var configuration = new ArchivizerConfiguration
            { 
                Achivizer7zFullName = globalConfiguration.Archivizer7zFullName,
                DeleteSourceFileAfterArchiving = globalConfiguration.DeleteSourceFileAfterArchiving,
                ArchivizerConfigurationsForDirectory = new ReadOnlyDictionary<string, ArchivizerConfigurationForDirectory>(configurations),
                MaxNumberOfLatestArchiveFilesInKept = globalConfiguration.MaxNumberOfLatestArchiveFilesInKept
            };

            ValidateConfiguration(configuration);
            return configuration;
        }

        private void ValidateConfiguration(ArchivizerConfiguration configuration)
        {
            if (!File.Exists(configuration.Achivizer7zFullName))
            {
                throw new ConfigurationErrorsException("Configuration is incorrect. Archiver not found in the indicated path");
            }
            foreach (var item in configuration.ArchivizerConfigurationsForDirectory.Values)
            {
                if (!Directory.Exists(item.DirectoryFullName))
                {
                    throw new ConfigurationErrorsException($"Configuration is incorrect. Directory {item.DirectoryFullName} not exist");
                }
            }
        }
    }
}