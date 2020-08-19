using System;
using System.Configuration;

namespace AppConfigurationManager.Configuration
{
    internal class ArchivizerConfigurationForDirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = false)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public String Name => (String)this["name"];

        [ConfigurationProperty("compressionLevel", IsRequired = true)]
        public String CompressionLevel => (String)this["compressionLevel"];

        [ConfigurationProperty("formatArchiwum", IsRequired = true)]
        public String FormatArchiwum => (String)this["formatArchiwum"];

        [ConfigurationProperty("directoryFullName", IsRequired = true, IsKey = true)]
        public String DirectoryFullName => (String)this["directoryFullName"];

        [ConfigurationProperty("fileExtensionToCompression", IsRequired = true)]
        public String FileExtensionToCompression => (String)this["fileExtensionToCompression"];
    }
}