namespace M2MCommunication.Core
{
    public class UaLibrarySettings
    {
        public string ConsumerConfigurationFile { get; set; }
        public string LibraryDirectory { get; set; }
        public string ResourcesDirectory { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
