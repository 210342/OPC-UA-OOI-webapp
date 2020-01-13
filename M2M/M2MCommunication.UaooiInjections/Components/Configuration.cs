using M2MCommunication.Core.CommonTypes;
using M2MCommunication.Core.Exceptions;
using M2MCommunication.Core.Interfaces;
using M2MCommunication.UaooiInjections.Extensions;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serializers;

namespace M2MCommunication.UaooiInjections.Components
{
    [Export(typeof(IConfigurationFactory))]
    [Export(typeof(IConfiguration))]
    public class Configuration : ConfigurationFactoryBase<ConfigurationExtension>, IConfiguration
    {
        private readonly ILogger _logger;
        protected readonly internal string _configurationFileName = string.Empty;

        public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
        public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

        [ImportingConstructor]
        public Configuration(
            ILogger logger,
            [Import(UaContractNames.ConfigurationFileNameContract)] string configurationFileName)
        {
            _configurationFileName = configurationFileName;
            _logger = logger;
            Loader = LoadConfig;
        }

        private ConfigurationExtension LoadConfig()
        {
            if (string.IsNullOrWhiteSpace(_configurationFileName))
            {
                ComponentNotInitialisedException exception = new ComponentNotInitialisedException($"{nameof(_configurationFileName)} was not initialised");
                _logger?.LogError(exception, exception.Message);
                throw exception;
            }

            FileInfo configurationFile = new FileInfo(_configurationFileName);
            if (configurationFile.Exists)
            {
                return ConfigurationDataFactoryIO.Load<ConfigurationExtension>(
                    SerializerType.Xml,
                    configurationFile,
                    (x, y, z) => _logger?.LogInfo($"{x}-{y}: {z}"),
                    RaiseEvents
                );
            }
            else
            {
                ConfigurationFileNotFoundException exception = new ConfigurationFileNotFoundException($"{nameof(Configuration)} could not find the file {_configurationFileName}", _configurationFileName);
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

        public string GetAliasForRepositoryGroup(string repositoryGroupName)
        {
            _logger?.LogInfo($"Looking for an alias for {repositoryGroupName}");
            if (Configuration is null)
            {
                return string.Empty;
            }
            UAOOI.Configuration.Networking.Serialization.DataSetConfiguration dataset =
                Configuration.DataSets?.FirstOrDefault(d =>
                    d.RepositoryGroup.ToLower().Equals(repositoryGroupName.ToLower()));
            return Configuration.Aliases?.FirstOrDefault(repoAlias =>
                repoAlias.InformationModelUri.ToString().Equals(dataset?.InformationModelURI))?.Alias
                    ?? string.Empty;
        }
    }
}
