using System;
using UAOOI.Configuration.Networking.Serialization;

namespace M2MCommunication.Uaooi.Extensions
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
                return typeInfo.ValueRank == 0 || typeInfo.ValueRank > 1;
            }
        }

        public static Type GetUAType(this UATypeInfo typeInfo)
        {
            if (typeInfo is null)
            {
                throw new ArgumentNullException(nameof(typeInfo));
            }

            switch (typeInfo.BuiltInType)
            {
                case BuiltInType.Boolean:
                    return typeof(bool);
                case BuiltInType.SByte:
                    return typeof(sbyte);
                case BuiltInType.Byte:
                    return typeof(byte);
                case BuiltInType.Int16:
                    return typeof(short);
                case BuiltInType.UInt16:
                    return typeof(ushort);
                case BuiltInType.Int32:
                    return typeof(int);
                case BuiltInType.UInt32:
                    return typeof(uint);
                case BuiltInType.Int64:
                    return typeof(long);
                case BuiltInType.UInt64:
                    return typeof(ulong);
                case BuiltInType.Float:
                    return typeof(float);
                case BuiltInType.Double:
                    return typeof(double);
                case BuiltInType.String:
                    return typeof(string);
                case BuiltInType.DateTime:
                    return typeof(DateTime?);
                case BuiltInType.Guid:
                    return typeof(Guid?);
                case BuiltInType.ByteString:
                    return typeof(byte[]);
                case BuiltInType.Null:
                case BuiltInType.XmlElement:
                case BuiltInType.NodeId:
                case BuiltInType.ExpandedNodeId:
                case BuiltInType.StatusCode:
                case BuiltInType.QualifiedName:
                case BuiltInType.LocalizedText:
                case BuiltInType.ExtensionObject:
                case BuiltInType.DataValue:
                case BuiltInType.Variant:
                case BuiltInType.DiagnosticInfo:
                case BuiltInType.Enumeration:
                default:
                    throw new UnsupportedTypeException(typeInfo.BuiltInType.ToString());
            }
        }
    }
}
