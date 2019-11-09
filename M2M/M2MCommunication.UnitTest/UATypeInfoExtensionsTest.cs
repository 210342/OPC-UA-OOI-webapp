using M2MCommunication.Core.Exceptions;
using M2MCommunication.Uaooi.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using UAOOI.Configuration.Networking.Serialization;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class UATypeInfoExtensionsTest
    {
        public static IEnumerable<object[]> ArrayTypes => new[]
            {
                new[] { new UATypeInfo(BuiltInType.Boolean, 0, new int[] { 42 }) },
                new[] { new UATypeInfo(BuiltInType.Boolean, 1, new int[] { 42 }) },
                new[] { new UATypeInfo(BuiltInType.Boolean, 2, new int[] { 3, 7 }) },
                new[] { new UATypeInfo(BuiltInType.Boolean, 4, new int[] { 2, 4, 8, 16}) }
            };

        public static IEnumerable<object[]> NonArrayTypes => new[]
            {
                new[] { new UATypeInfo(BuiltInType.Boolean, -1) }
            };

        public static IEnumerable<object[]> MultidimensionalArrayTypes => new[]
            {
                new[] { new UATypeInfo(BuiltInType.Boolean, 0, new int[] { 21, 37 }) },
                new[] { new UATypeInfo(BuiltInType.Boolean, 2, new int[] { 21, 42 }) },
                new[] { new UATypeInfo(BuiltInType.Boolean, 4, new int[] { 2, 4, 8, 16 }) }
            };

        public static IEnumerable<object[]> NonMultidimensionalArrayTypes => new[]
            {
                new[] { new UATypeInfo(BuiltInType.Boolean, 1, new int[] { 42 }) },
                new[] { new UATypeInfo(BuiltInType.Boolean, -1) }
            };

        public static IEnumerable<object[]> BuiltInTypes => new[]
            {
                new object[] { BuiltInType.Boolean, typeof(bool) },
                new object[] { BuiltInType.SByte, typeof(sbyte) },
                new object[] { BuiltInType.Int16, typeof(short) },
                new object[] { BuiltInType.Int32, typeof(int) },
                new object[] { BuiltInType.Int64, typeof(long) },
                new object[] { BuiltInType.Byte, typeof(byte) },
                new object[] { BuiltInType.UInt16, typeof(ushort) },
                new object[] { BuiltInType.UInt32, typeof(uint) },
                new object[] { BuiltInType.UInt64, typeof(ulong) },
                new object[] { BuiltInType.Float, typeof(float) },
                new object[] { BuiltInType.Double, typeof(double) },
                new object[] { BuiltInType.String, typeof(string) },
                new object[] { BuiltInType.DateTime, typeof(DateTime?) },
                new object[] { BuiltInType.Guid, typeof(Guid?) },
                new object[] { BuiltInType.ByteString, typeof(byte[]) }
            };

        public static IEnumerable<object[]> UnsupportedBuiltInTypes => new[]
            {
                new object[] { BuiltInType.Null },
                new object[] { BuiltInType.XmlElement },
                new object[] { BuiltInType.NodeId },
                new object[] { BuiltInType.ExpandedNodeId },
                new object[] { BuiltInType.StatusCode },
                new object[] { BuiltInType.QualifiedName },
                new object[] { BuiltInType.LocalizedText },
                new object[] { BuiltInType.ExtensionObject },
                new object[] { BuiltInType.DataValue },
                new object[] { BuiltInType.Variant },
                new object[] { BuiltInType.DiagnosticInfo },
                new object[] { BuiltInType.Enumeration } 
            };

        [Theory]
        [MemberData(nameof(ArrayTypes))]
        public void ContainsArrayForArrayTypesTest(UATypeInfo typeInfo)
        {
            Assert.True(typeInfo.ContainsArray());
        }

        [Theory]
        [MemberData(nameof(NonArrayTypes))]
        public void ContainsArrayForNonArrayTypesTest(UATypeInfo typeInfo)
        {
            Assert.False(typeInfo.ContainsArray());
        }

        [Theory]
        [MemberData(nameof(MultidimensionalArrayTypes))]
        public void ContainsMultidimensionalArrayForMultidimensionalArrayTypesTest(UATypeInfo typeInfo)
        {
            Assert.True(typeInfo.ContainsArray());
        }

        [Theory]
        [MemberData(nameof(BuiltInTypes))]
        public void GetUATypeTest(BuiltInType builtInType, Type type)
        {
            Assert.Equal(type, new UATypeInfo(builtInType).GetUAType());
        }

        [Theory]
        [MemberData(nameof(UnsupportedBuiltInTypes))]
        public void GetUATypeForUnsupportedTypeTest(BuiltInType builtInType)
        {
            Assert.Throws<UnsupportedTypeException>(() => new UATypeInfo(builtInType).GetUAType());
        }
    }
}
