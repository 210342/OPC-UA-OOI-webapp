using ReactiveHMI.M2MCommunication.Core.CommonTypes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ReactiveHMI.M2MCommunicationUnitTest
{
    public class UaTypeMetadataTest
    {
        public static IEnumerable<object[]> ConstructorInvalidTypeNameData =>
        [
            [null],
            [""],
            ["  "],
            [Environment.NewLine]
        ];

        public static IEnumerable<object[]> EqualsTestData =>
        [
            [new UaTypeMetadata("repo", "test"), new UaTypeMetadata("repo", "test"), true],
            [new UaTypeMetadata("repo", "test2"), new UaTypeMetadata("repo", "test"), false],
            [new UaTypeMetadata("repo2", "test"), new UaTypeMetadata("repo", "test"), false],
            [new UaTypeMetadata("repo", "test"), null, false]
        ];

        [Theory]
        [MemberData(nameof(ConstructorInvalidTypeNameData))]
        public void ConstructorInvalidTypeNameTest(string typeName)
        {
            Assert.Throws<ArgumentNullException>(() => new UaTypeMetadata("repo", typeName));
        }

        [Fact]
        public void ConstructorValidDataTest()
        {
            UaTypeMetadata sut = new("repo", "type");
            Assert.Equal("repo", sut.RepositoryGroupName);
            Assert.Equal("type", sut.TypeName);
        }

        [Fact]
        public void ConstructorNullRepositoryNameTest()
        {
            UaTypeMetadata sut = new(null, "type");
            Assert.Equal(string.Empty, sut.RepositoryGroupName);
            Assert.Equal("type", sut.TypeName);
        }

        [Fact]
        public void ToStringTest()
        {
            UaTypeMetadata sut = new("repo", "type");
            Assert.Equal("repo.type", sut.ToString());
        }

        [Fact]
        public void HashCodeTest()
        {
            UaTypeMetadata sut = new("repo", "type");
            UaTypeMetadata sut2 = new("repo", "type");
            Assert.Equal(sut.GetHashCode(), sut2.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(EqualsTestData))]
        public void EqualsTest(UaTypeMetadata type1, UaTypeMetadata type2, bool expectedResult)
        {
            Assert.Equal(expectedResult, type1.Equals(type2));
        }
    }
}
