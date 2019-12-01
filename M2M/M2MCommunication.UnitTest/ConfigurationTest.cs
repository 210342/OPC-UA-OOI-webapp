using M2MCommunication.Core.Exceptions;
using M2MCommunication.Uaooi.Injections;
using System.Reflection;
using UAOOI.Configuration.Networking.Serialization;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class ConfigurationTest
    {
        private static readonly string _configurationFileName = @"M2MCommunication.UAOOI\ConfigurationDataConsumer.xml";
        [Fact]
        public void ConstructorTest()
        {
            Configuration configuration = new Configuration();
            Assert.NotNull(configuration
                .GetType()
                .GetProperty("Loader", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(configuration));
        }

        [Fact]
        public void InitialiseTest()
        {
            Configuration configuration = new Configuration();
            configuration.Initialise(_configurationFileName);
            Assert.Equal(_configurationFileName, configuration
                .GetType()
                .GetField("_configurationFileName", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(configuration)
            );
        }

        [Fact]
        public void InitialiseWrongFileNameTest()
        {
            Configuration configuration = new Configuration();
            Assert.Throws<ConfigurationFileNotFoundException>(() => 
            configuration.Initialise(_configurationFileName.Replace("xml", "pdf")));
        }

        [Fact]
        public void LoadConfigTest()
        {
            Configuration configuration = new Configuration();
            configuration.Initialise(_configurationFileName);
            Assert.NotNull(LoadConfig(configuration));
        }

        private ConfigurationData LoadConfig(Configuration config)
        {
            return config.GetType()
                .GetMethod("LoadConfig", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(config, new object[] { })
                as ConfigurationData;
        }
    }
}
