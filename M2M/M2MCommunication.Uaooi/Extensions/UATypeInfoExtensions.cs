using M2MCommunication.Core.Exceptions;
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

        public static Type GetUAType(this UATypeInfo typeInfo)
        {
            if (typeInfo is null)
            {
                throw new ArgumentNullException(nameof(typeInfo));
            }

            switch (typeInfo.BuiltInType)
            {
                case BuiltInType.Boolean:
                    return typeInfo.ValueRank == 1 ? typeof(bool[]) : typeof(bool);
                case BuiltInType.SByte:
                    return typeInfo.ValueRank == 1 ? typeof(sbyte[]) : typeof(sbyte);
                case BuiltInType.Byte:
                    return typeInfo.ValueRank == 1 ? typeof(byte[]) : typeof(byte);
                case BuiltInType.Int16:
                    return typeInfo.ValueRank == 1 ? typeof(short[]) : typeof(short);
                case BuiltInType.UInt16:
                    return typeInfo.ValueRank == 1 ? typeof(ushort[]) : typeof(ushort);
                case BuiltInType.Int32:
                    return typeInfo.ValueRank == 1 ? typeof(int[]) : typeof(int);
                case BuiltInType.UInt32:
                    return typeInfo.ValueRank == 1 ? typeof(uint[]) : typeof(int);
                case BuiltInType.Int64:
                    return typeInfo.ValueRank == 1 ? typeof(long[]) : typeof(long);
                case BuiltInType.UInt64:
                    return typeInfo.ValueRank == 1 ? typeof(ulong[]) : typeof(ulong);
                case BuiltInType.Float:
                    return typeInfo.ValueRank == 1 ? typeof(float[]) : typeof(float);
                case BuiltInType.Double:
                    return typeInfo.ValueRank == 1 ? typeof(double[]) : typeof(double);
                case BuiltInType.String:
                    return typeInfo.ValueRank == 1 ? typeof(string[]) : typeof(string);
                case BuiltInType.DateTime:
                    return typeInfo.ValueRank == 1 ? typeof(DateTime?[]) : typeof(DateTime?);
                case BuiltInType.Guid:
                    return typeInfo.ValueRank == 1 ? typeof(Guid?[]) : typeof(Guid?);
                case BuiltInType.ByteString:
                    return typeInfo.ValueRank == 1 ? typeof(byte[][]) : typeof(byte[]);
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
