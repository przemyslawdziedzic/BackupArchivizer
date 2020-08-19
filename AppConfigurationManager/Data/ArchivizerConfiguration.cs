using System.Collections.ObjectModel;

namespace AppConfigurationManager.Data
{
    public class ArchivizerConfiguration
    {
        public string Achivizer7zFullName { get; internal set; }
        public ReadOnlyDictionary<string, ArchivizerConfigurationForDirectory> ArchivizerConfigurationsForDirectory { get; internal set; }
        public bool DeleteSourceFileAfterArchiving { get; internal set; }
        public int? MaxNumberOfLatestArchiveFilesInKept { get; internal set; }
    }
}
