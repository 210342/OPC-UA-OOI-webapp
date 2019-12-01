using M2MCommunication.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class UaTypeMetadataTest
    {
        public static IEnumerable<object[]> ConstructorInvalidTypeNameData => new[]
        {
            new object[] { null },
            new object[] { "" },
            new object[] { "  " },
            new object[] { Environment.NewLine }
        };

        public static IEnumerable<object[]> EqualsTestData => new[]
        {
            new object[] { new UaTypeMetadata("repo", "test"), new UaTypeMetadata("repo", "test"), true },
            new object[] { new UaTypeMetadata("repo", "test2"), new UaTypeMetadata("repo", "test"), false },
            new object[] { new UaTypeMetadata("repo2", "test"), new UaTypeMetadata("repo", "test"), false },
            new object[] { new UaTypeMetadata("repo", "test"), null, false }
        };

        [Theory]
        [MemberData(nameof(ConstructorInvalidTypeNameData))]
        public void ConstructorInvalidTypeNameTest(string typeName)
        {
            Assert.Throws<ArgumentNullException>(() => new UaTypeMetadata("repo", typeName));
        }

        [Fact]
        public void ConstructorValidDataTest()
        {
            UaTypeMetadata sut = new UaTypeMetadata("repo", "type");
            Assert.Equal("repo", sut.RepositoryGroupName);
            Assert.Equal("type", sut.TypeName);
        }

        [Fact]
        public void ConstructorNullRepositoryNameTest()
        {
            UaTypeMetadata sut = new UaTypeMetadata(null, "type");
            Assert.Equal(string.Empty, sut.RepositoryGroupName);
            Assert.Equal("type", sut.TypeName);
        }

        [Fact]
        public void ToStringTest()
        {
            UaTypeMetadata sut = new UaTypeMetadata("repo", "type");
            Assert.Equal("repo.type", sut.ToString());
        }

        [Fact]
        public void HashCodeTest()
        {
            UaTypeMetadata sut = new UaTypeMetadata("repo", "type");
            UaTypeMetadata sut2 = new UaTypeMetadata("repo", "type");
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
