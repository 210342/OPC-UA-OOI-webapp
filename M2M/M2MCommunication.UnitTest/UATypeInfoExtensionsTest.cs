using ReactiveHMI.M2MCommunication.UaooiInjections.Extensions;
using System.Collections.Generic;
using UAOOI.Configuration.Networking.Serialization;
using Xunit;

namespace ReactiveHMI.M2MCommunicationUnitTest
{
    public class UATypeInfoExtensionsTest
    {
        public static IEnumerable<object[]> ArrayTypes =>
            [
                [new UATypeInfo(BuiltInType.Boolean, 0, [42])],
                [new UATypeInfo(BuiltInType.Boolean, 1, [42])],
                [new UATypeInfo(BuiltInType.Boolean, 2, [3, 7])],
                [new UATypeInfo(BuiltInType.Boolean, 4, [2, 4, 8, 16])]
            ];

        public static IEnumerable<object[]> NonArrayTypes =>
            [
                [new UATypeInfo(BuiltInType.Boolean, -1)]
            ];

        public static IEnumerable<object[]> MultidimensionalArrayTypes =>
            [
                [new UATypeInfo(BuiltInType.Boolean, 0, [21, 37])],
                [new UATypeInfo(BuiltInType.Boolean, 2, [21, 42])],
                [new UATypeInfo(BuiltInType.Boolean, 4, [2, 4, 8, 16])]
            ];

        public static IEnumerable<object[]> NonMultidimensionalArrayTypes =>
            [
                [new UATypeInfo(BuiltInType.Boolean, 1, [42])],
                [new UATypeInfo(BuiltInType.Boolean, -1)]
            ];

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
            Assert.True(typeInfo.ContainsMultidimensionalArray());
        }

        [Theory]
        [MemberData(nameof(NonMultidimensionalArrayTypes))]
        public void ContainsMultidimensionalArrayForNonMultidimensionalArrayTypesTest(UATypeInfo typeInfo)
        {
            Assert.False(typeInfo.ContainsMultidimensionalArray());
        }
    }
}
