using System;

namespace M2MCommunication.Core.CommonTypes
{
    public class UaLibrarySettings
    {
        public string ConsumerConfigurationFile { get; set; }
        public string LibraryDirectory { get; set; }
        public string ResourcesDirectory { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(ConsumerConfigurationFile, LibraryDirectory, ResourcesDirectory);
        }

        public override bool Equals(object obj)
        {
            return obj is UaLibrarySettings settings
                && settings.LibraryDirectory.Equals(LibraryDirectory)
                && settings.ResourcesDirectory.Equals(ResourcesDirectory)
                && settings.ConsumerConfigurationFile.Equals(ConsumerConfigurationFile);
        }
    }
}
