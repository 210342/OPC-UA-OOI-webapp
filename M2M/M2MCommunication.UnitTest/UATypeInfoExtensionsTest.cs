using ReactiveHMI.M2MCommunication.UaooiInjections.Extensions;
using System.Collections.Generic;
using UAOOI.Configuration.Networking.Serialization;
using Xunit;

namespace ReactiveHMI.M2MCommunicationUnitTest
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
