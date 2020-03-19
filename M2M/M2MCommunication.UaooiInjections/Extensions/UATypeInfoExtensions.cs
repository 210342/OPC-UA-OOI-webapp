using UAOOI.Configuration.Networking.Serialization;

namespace ReactiveHMI.M2MCommunication.UaooiInjections.Extensions
{
    public static class UATypeInfoExtensions
    {
        public static bool ContainsArray(this UATypeInfo typeInfo)
        {
            if (typeInfo is null)
            {
                return false;
            }
            else
            {
                return typeInfo.ValueRank >= 0;
            }
        }

        public static bool ContainsMultidimensionalArray(this UATypeInfo typeInfo)
        {
            if (typeInfo is null)
            {
                return false;
            }
            else
            {
                return typeInfo.ValueRank == 0 || typeInfo.ValueRank > 1;
            }
        }
    }
}
