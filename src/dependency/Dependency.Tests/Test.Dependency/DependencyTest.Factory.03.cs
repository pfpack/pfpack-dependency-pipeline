#nullable enable

using System;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.Tests
{
    partial class DependencyTest
    {
        [Fact]
        public void Create_03_FirstIsNull_ExpectArgumentNullException()
        {
            var second = MinusFifteenIdRefType;
            var third = SomeTextStructType;

            var ex = Assert.Throws<ArgumentNullException>(
                () => _ = Dependency.Create(NullRecordResolver, _ => second, _ => third));

            Assert.Equal("first", ex.ParamName);
        }

        [Fact]
        public void Create_03_SecondIsNull_ExpectArgumentNullException()
        {
            var first = SomeString;
            var third = MinusFifteenIdSomeStringNameRecord;

            var ex = Assert.Throws<ArgumentNullException>(
                () => _ = Dependency.Create(_ => first, NullStructResolver, _ => third));

            Assert.Equal("second", ex.ParamName);
        }

        [Fact]
        public void Create_03_ThirdIsNull_ExpectArgumentNullException()
        {
            var first = ZeroIdNullNameRecord;
            var second = SomeTextStructType;

            var ex = Assert.Throws<ArgumentNullException>(
                () => _ = Dependency.Create(_ => first, _ => second, NullRecordResolver));

            Assert.Equal("third", ex.ParamName);
        }

        [Theory]
        [MemberData(nameof(TestEntitySource.RecordTypes), MemberType = typeof(TestEntitySource))]
        public void Create_03_ResolversAreNotNull_ExpectResolvedValuesAreSameAsSource(
            RecordType sourceThird)
        {
            var sourceFirst = MinusFifteenIdRefType;
            var sourceSecond = SomeTextStructType;

            var actual = Dependency.Create(_ => sourceFirst, _ => sourceSecond, _ => sourceThird);

            var serviceProvider = CreateServiceProvider();
            var actualValue = actual.Resolve(serviceProvider);

            var expectedValue = (sourceFirst, sourceSecond, sourceThird);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}