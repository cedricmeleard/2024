using System.Text;
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
        [InlineData(10, 1_023)]
        [InlineData(20, 1_048_575)]
        [InlineData(30, 1_073_741_823)]
        [InlineData(32, 4_294_967_295)]
        [InlineData(50, 1_125_899_906_842_623)]
        [InlineData(53, 9_007_199_254_740_991)]
        public void Should_Calculate_The_DistanceFor(int numberOfReindeers, ulong expectedDistance)
            => CalculateTotalDistance(numberOfReindeers)
                .Should()
                .Be(expectedDistance);
        
        [Property]
        public Property Should_Calculate_The_DistanceFor_Any_Number_Of_Reindeers()
        {
            return Prop.ForAll(
                Gen.Choose(1, 53).ToArbitrary(),
                numberOfReindeers
                    => CalculateTotalDistance(numberOfReindeers)
                        .Should()
                        .BeInRange(1, ulong.MaxValue)
            );
        }
        
        [Property]
        public void Should_Not_Calculate_The_DistanceFor_Any_Negative_Number_Of_Reindeers(NegativeInt numberOfReindeers)
        {
            var act = () => CalculateTotalDistance(numberOfReindeers.Get);
            act
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The number of reindeers must be greater than 0 (Parameter 'numberOfReindeers')");
        }
        
        [Property]
        public Property Should_Not_Calculate_The_DistanceFor_Any_Number_Of_Reindeers_GreaterThan53()
            => Prop.ForAll(
                Gen.Choose(54, int.MaxValue).ToArbitrary(),
                numberOfReindeers =>
                {
                    var act = () => CalculateTotalDistance(numberOfReindeers);
                    act
                        .Should()
                        .Throw<ArgumentException>()
                        .WithMessage("The number of reindeers must be less than or equal to 53 (Parameter 'numberOfReindeers')");
                });
        
        [Fact]
        public async Task Verify_Calculate_The_DistanceFor_Reindeers_Recursively()
        {
            var sb = new StringBuilder();

            for (var numberOfReindeers = 1; numberOfReindeers <= 53; numberOfReindeers++)
            {
                sb.AppendLine($"{numberOfReindeers} => {CalculateTotalDistance(numberOfReindeers)},");
            }
            
            await Verify(sb.ToString());
        }
    }
}