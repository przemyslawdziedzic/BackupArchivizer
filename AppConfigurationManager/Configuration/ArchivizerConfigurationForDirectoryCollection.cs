using System;
using System.Configuration;

namespace AppConfigurationManager.Configuration
{
    internal class ArchivizerConfigurationForDirectoryCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

        protected override ConfigurationElement CreateNewElement() => new ArchivizerConfigurationForDirectoryElement();

        protected override Object GetElementKey(ConfigurationElement element) => ((ArchivizerConfigurationForDirectoryElement)element).Name;

        public ArchivizerConfigurationForDirectoryElement this[int index] => (ArchivizerConfigurationForDirectoryElement)BaseGet(index);

        new public ArchivizerConfigurationForDirectoryElement this[string name] => (ArchivizerConfigurationForDirectoryElement)BaseGet(name);

        public int IndexOf(ArchivizerConfigurationForDirectoryElement details) => BaseIndexOf(details);

        protected override string ElementName => "ArchivizerConfigurationForDirectory";
    }
}
