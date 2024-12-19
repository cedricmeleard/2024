using Ardalis.GuardClauses;

namespace Travel
{
public static class SantaTravelCalculator
{
    private const string TheNumberOfReindeersMustBeLessThanOrEqualTo = "The number of reindeers must be less than or equal to 63";
    private const string TheNumberOfReindeersMustBeGreaterThan = "The number of reindeers must be greater than 0";
    private const int MaxReindeer = 63;

    public static ulong CalculateTotalDistance(int numberOfReindeers)
    {
        EnsureValidReindeerNumber(numberOfReindeers);

        return (ulong)(Math.Pow(2, numberOfReindeers) - 1);
    }

    private static void EnsureValidReindeerNumber(int numberOfReindeers)
    {
        Guard.Against.NegativeOrZero(
            input: numberOfReindeers, 
            message: TheNumberOfReindeersMustBeGreaterThan);
        
        Guard.Against.Expression(
            func: x => x > MaxReindeer,  
            input: numberOfReindeers, 
            message: TheNumberOfReindeersMustBeLessThanOrEqualTo);
    }
}
}