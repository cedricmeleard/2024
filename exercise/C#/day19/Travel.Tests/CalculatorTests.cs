using FluentAssertions;
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
        public void Should_Calculate_The_DistanceFor(int numberOfReindeers, long expectedDistance)
            => CalculateTotalDistanceRecursively(numberOfReindeers)
                .Should()
                .Be(expectedDistance);

    }
}