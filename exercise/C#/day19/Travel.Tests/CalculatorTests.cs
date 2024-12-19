using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static Travel.SantaTravelCalculator;

namespace Travel.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(5, 31)]
        [InlineData(10, 1023)]
        [InlineData(20, 1048575)]
        [InlineData(30, 1073741823)]
        [InlineData(32, 4294967295)]
        [InlineData(50, 1125899906842623)]
        [InlineData(63, 9223372036854775808)]
        public void Should_Calculate_The_DistanceFor(int numberOfReindeers, ulong expectedDistance)
            => CalculateTotalDistance(numberOfReindeers)
                .Should()
                .Be(expectedDistance);
        
        [Property]
        public Property Should_Calculate_The_DistanceFor_Any_Number_Of_Reindeers()
        {
            return Prop.ForAll(
                Gen.Choose(1, 63).ToArbitrary(),
                numberOfReindeers
                    => CalculateTotalDistance(numberOfReindeers)
                        .Should()
                        .BeInRange(1, ulong.MaxValue)
            );
        }
    }
}