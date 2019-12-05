using M2MCommunication.Core;
using M2MCommunication.Core.Exceptions;
using M2MCommunication.Uaooi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serializers;

namespace M2MCommunication.Uaooi.Injections
{
    [Export(typeof(IConfigurationFactory))]
    [Export(typeof(IConfiguration))]
    public class Configuration : ConfigurationFactoryBase<ConfigurationExtension>, IConfiguration
    {
        private readonly ILogger _logger;
        protected internal string _configurationFileName = string.Empty;

        public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
        public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

        [ImportingConstructor]
        public Configuration(ILogger logger)
        {
            _logger = logger;
            Loader = LoadConfig;
        }

        /// <summary>
        /// Initialises neccessary values not provided through dependency injection
        /// </summary>
        /// <param name="configurationFileName">name of the file containing the consumer's configuration</param>
        public void Initialise(string configurationFileName)
        {
            _configurationFileName = configurationFileName;
            GetConfiguration();
        }

        private ConfigurationExtension LoadConfig()
        {
            if (string.IsNullOrWhiteSpace(_configurationFileName))
            {
                var exception = new ComponentNotInitialisedException($"{nameof(_configurationFileName)} was not initialised");
                _logger?.LogError(exception, exception.Message);
                throw exception;
            }

            FileInfo configurationFile = new FileInfo(_configurationFileName);
            if (configurationFile.Exists)
            {
                return ConfigurationDataFactoryIO.Load(
                    () => XmlDataContractSerializers.Load<ConfigurationExtension>(
                        configurationFile,
                        (x, y, z) => _logger?.LogInfo($"{x}-{y}: {z}")), 
                    RaiseEvents);
            }
            else
            {
                var exception = new ConfigurationFileNotFoundException($"{nameof(Configuration)} could not find the file {_configurationFileName}", _configurationFileName);
                _logger?.LogError(exception, exception.Message);
                throw exception;
            }
        }

        protected override void RaiseEvents()
        {
            _logger?.LogInfo("IConfiguration events invoked");
            OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
            OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
        }

        public IDictionary<string, string> GetRepositoryGroupAliases()
        {
            _logger?.LogInfo("Aliases requested");
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (UAOOI.Configuration.Networking.Serialization.DataSetConfiguration dataset in Configuration.DataSets)
            {
                string alias = Configuration
                    ?.Aliases
                    ?.FirstOrDefault(repoAlias => repoAlias.InformationModelUri.ToString().Equals(dataset.InformationModelURI))
                    ?.Alias;
                if (!string.IsNullOrEmpty(alias))
                {
                    result.Add(dataset.RepositoryGroup, alias);
                }
            }
            return result;
        }
    }
}
