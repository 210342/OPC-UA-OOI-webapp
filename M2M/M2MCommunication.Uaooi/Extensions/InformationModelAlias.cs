using System;
using System.Runtime.Serialization;

namespace M2MCommunication.Uaooi.Extensions
{
    [DataContract(Namespace = "http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/Serialization.xsd")]
    public class InformationModelAlias
    {
        [DataMember(Name = "InformationModelURI", IsRequired = true)]
        public Uri InformationModelUri { get; set; }
        [DataMember(Name = "Alias", EmitDefaultValue = true, IsRequired = true)]
        public string Alias { get; set; }
    }
}
