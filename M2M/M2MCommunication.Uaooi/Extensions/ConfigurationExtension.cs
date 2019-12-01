using System.Collections.Generic;
using System.Runtime.Serialization;
using UAOOI.Configuration.Networking.Serialization;

namespace M2MCommunication.Uaooi.Extensions
{
    [DataContract(Name = "ConfigurationData", Namespace = "http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/Serialization.xsd")]
    public class ConfigurationExtension : ConfigurationData, IExtensibleDataObject
    {
        [DataMember(Name = "InformationModelAliases", EmitDefaultValue = true, IsRequired = false)]
        private readonly InformationModelAlias[] _aliases = new InformationModelAlias[0];

        public IEnumerable<InformationModelAlias> Aliases => _aliases;
    }
}
