using M2MCommunication.Core;
using M2MCommunication.Uaooi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Serializers;

namespace M2MCommunication.Uaooi.Injections
{
    [Export(typeof(IConfiguration))]
    public class Configuration : ConfigurationFactoryBase<ConfigurationData>, IConfiguration
    {
        protected internal string _configurationFileName;

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
        }

        private ConfigurationData LoadConfig()
        {
            FileInfo configurationFile = new FileInfo(_configurationFileName);
            if (configurationFile.Exists)
            {
                return ConfigurationDataFactoryIO.Load(() => XmlDataContractSerializers.Load<ConfigurationData>(configurationFile, (x, y, z) => { }), RaiseEvents);
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

        public IEnumerable<string> GetDataTypeNames()
        {
            return Configuration
                ?.DataSets
                ?.SelectMany(dataset => dataset.DataSet)
                ?.Select(fieldMetadata => fieldMetadata.ProcessValueName)
                    ?? Enumerable.Empty<string>();
        }
    }
}
