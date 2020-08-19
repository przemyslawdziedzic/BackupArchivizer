using System.Configuration;

namespace AppConfigurationManager.Configuration
{
    internal class GlobalConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("archivizer7zFullName", IsRequired = true, IsKey = true)]
        public string Archivizer7zFullName
        {
            get
            {
                return (string)this["archivizer7zFullName"];
            }
            set
            {
                this["archivizer7zFullName"] = value;
            }
        }

        [ConfigurationProperty("deleteSourceFileAfterArchiving", IsRequired = true, IsKey = true)]
        public bool DeleteSourceFileAfterArchiving
        {
            get
            {
                return (bool)this["deleteSourceFileAfterArchiving"];
            }
            set
            {
                this["deleteSourceFileAfterArchiving"] = value;
            }
        }

        [ConfigurationProperty("maxNumberOfLatestArchiveFilesInKept", DefaultValue = -1)]
        public int? MaxNumberOfLatestArchiveFilesInKept
        {
            get
            {
                return (int)this["maxNumberOfLatestArchiveFilesInKept"] < 1 ? (int?)null : (int)this["maxNumberOfLatestArchiveFilesInKept"];
            }
            set
            {
                this["maxNumberOfLatestArchiveFilesInKept"] = value;
            }
        }
    }
}
