#nullable enable

using System;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.Tests
{
    partial class TwoDependencyTest
    {
        [Fact]
        public void WithTwo_OtherIsNull_ExpectArgumentNullException()
        {
            var source = Dependency.Create(_ => SomeTextStructType, _ => PlusFifteenIdRefType);
            var other = null as Dependency<RecordType?, DateTime>;

            var ex = Assert.Throws<ArgumentNullException>(
                () => _ = source.With(other!));
            
            Assert.Equal("other", ex.ParamName);
        }

        [Theory]
        [MemberData(nameof(TestEntitySource.RefTypes), MemberType = typeof(TestEntitySource))]
        public void WithTwo_OtherIsNotNull_ExpectResolvedValuesAreSameAsSourceAndOther(
            RefType otherLast)
        {
            var firstSource = ZeroIdNullNameRecord;
            var secondSource = SomeTextStructType;

            var source = Dependency.Create(_ => firstSource, _ => secondSource);

            var otherFirst = PlusFifteenIdRefType;
            var other = Dependency.Create(_ => otherFirst, _ => otherLast);

            var actual = source.With(other);
            var actualValue = actual.Resolve();

            var expectedValue = (firstSource, secondSource, otherFirst, otherLast);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}