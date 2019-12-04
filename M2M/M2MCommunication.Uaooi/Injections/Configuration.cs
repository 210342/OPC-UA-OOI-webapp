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
        protected internal string _configurationFileName = string.Empty;

        public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
        public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;

        public Configuration()
        {
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
                throw new ComponentNotInitialisedException($"{nameof(_configurationFileName)} was not initialised");
            }

            FileInfo configurationFile = new FileInfo(_configurationFileName);
            if (configurationFile.Exists)
            {
                return ConfigurationDataFactoryIO.Load(() => XmlDataContractSerializers.Load<ConfigurationExtension>(configurationFile, (x, y, z) => { }), RaiseEvents);
            }
            else
            {
                throw new ConfigurationFileNotFoundException($"{nameof(Configuration)} could not find the file {_configurationFileName}", _configurationFileName);
            }
        }

        protected override void RaiseEvents()
        {
            OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
            OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
        }

        public IDictionary<string, string> GetRepositoryGroupAliases()
        {
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
