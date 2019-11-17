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
            configuration.Initialise(_configurationFileName.Replace("xml", "pdf"));
            try
            {
                LoadConfig(configuration);
            }
            catch (TargetInvocationException ex)
            {
                Assert.IsType<ConfigurationFileNotFoundException>(ex.InnerException);
            }
        }

        [Fact]
        public void LoadConfigTest()
        {
            Configuration configuration = new Configuration();
            configuration.Initialise(_configurationFileName);
            Assert.NotNull(LoadConfig(configuration));
        }

        [Fact]
        public void GetDataTypeNamesEmptyTest()
        {
            Assert.Throws<ComponentNotIntialisedException>(() => new Configuration().GetTypeMetadata());
        }

        [Fact]
        public void GetDataTypeNamesNotEmptyTest()
        {
            Configuration configuration = new Configuration();
            configuration.Initialise(_configurationFileName);
            LoadConfig(configuration);
            Assert.NotEmpty(configuration.GetTypeMetadata());
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
