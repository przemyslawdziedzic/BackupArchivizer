using System;
using AppConfigurationManager.Configuration;
using BackupArchivizer;

namespace AppConfigurationManager.Data
{
    public class ArchivizerConfigurationForDirectory
    {
        public String Name { get; private set; }
        public CompressionLevel CompressionLevel { get; private set; }
        public String DirectoryFullName { get; private set; }
        public String FormatArchiwum { get; private set; }
        public String FileExtensionToCompression { get; private set; }

        internal ArchivizerConfigurationForDirectory(ArchivizerConfigurationForDirectoryElement configuration)
        {
            Name = configuration.Name;
            CompressionLevel = MapCompressionLevel(configuration.CompressionLevel);
            FormatArchiwum = configuration.FormatArchiwum;
            DirectoryFullName = configuration.DirectoryFullName;
            FileExtensionToCompression = configuration.FileExtensionToCompression;
        }

        private CompressionLevel MapCompressionLevel(string compressionLevel)
        {
            switch (compressionLevel)
            {
                case "Fastest":
                    return CompressionLevel.Fasttest;
                case "Fast":
                    return CompressionLevel.Fast;
                case "Normal":
                    return CompressionLevel.Normal;
                case "Maximum":
                    return CompressionLevel.Maximum;
                case "Ultra":
                    return CompressionLevel.Ultra;
                default:
                    return CompressionLevel.Store;
            }
        }
    }
}

/*
 *       <xs:enumeration value="Store" />
      <xs:enumeration value="Fastest" />
      <xs:enumeration value="Fast" />
      <xs:enumeration value="Normal" />
      <xs:enumeration value="Maximum" />
      <xs:enumeration value="Ultra" />
 * 
 */
