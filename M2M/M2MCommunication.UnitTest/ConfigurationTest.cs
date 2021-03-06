﻿using ReactiveHMI.M2MCommunication.Core.Exceptions;
using ReactiveHMI.M2MCommunication.UaooiInjections.Components;
using System.IO;
using System.Reflection;
using UAOOI.Configuration.Networking.Serialization;
using Xunit;

namespace ReactiveHMI.M2MCommunicationUnitTest
{
    public class ConfigurationTest
    {
        private static readonly string _configurationFileName = 
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                "M2MCommunication.Uaooi", 
                "ConfigurationDataConsumer.xml"
            );

        [Fact]
        public void ConstructorTest()
        {
            Configuration configuration = new Configuration(null, _configurationFileName);
            Assert.NotNull(configuration
                .GetType()
                .GetProperty("Loader", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(configuration));
            Assert.Equal(_configurationFileName, configuration
                .GetType()
                .GetField("_configurationFileName", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(configuration)
            );
        }

        [Fact]
        public void ConstructorWrongFileNameTest()
        {
            Configuration configuration = new Configuration(null, _configurationFileName.Replace("xml", "pdf"));
            Assert.Throws<ConfigurationFileNotFoundException>(() =>
            configuration.GetConfiguration());
        }

        [Fact]
        public void LoadConfigTest()
        {
            Configuration configuration = new Configuration(null, _configurationFileName);
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
