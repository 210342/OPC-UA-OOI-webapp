using M2MCommunication.Uaooi.Injections;
using M2MCommunication.Uaooi.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using M2MCommunication.Core.Exceptions;
using UAOOI.Configuration.Networking.Serialization;

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
            Assert.Throws<ComponentNotIntialisedException>(() => new Configuration().GetDataTypeNames());
        }

        [Fact]
        public void GetDataTypeNamesNotEmptyTest()
        {
            Configuration configuration = new Configuration();
            configuration.Initialise(_configurationFileName);
            LoadConfig(configuration);
            Assert.NotEmpty(configuration.GetDataTypeNames());
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
