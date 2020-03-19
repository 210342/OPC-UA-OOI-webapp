using System;

namespace ReactiveHMI.M2MCommunication.Core.CommonTypes
{
    public class UaTypeMetadata
    {
        public string TypeName { get; }
        public string RepositoryGroupName { get; }

        public UaTypeMetadata(string repositoryGroupName, string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException(nameof(typeName));
            }
            TypeName = typeName;
            RepositoryGroupName = repositoryGroupName ?? string.Empty;
        }

        public override string ToString()
        {
            return $"{RepositoryGroupName}.{TypeName}";
        }

        public override bool Equals(object obj)
        {
            if (obj is UaTypeMetadata typeMetadata)
            {
                return TypeName.Equals(typeMetadata.TypeName)
                    && RepositoryGroupName.Equals(typeMetadata.RepositoryGroupName);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TypeName, RepositoryGroupName);
        }
    }
}
