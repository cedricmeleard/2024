using Ardalis.GuardClauses;

namespace Travel
{
public static class SantaTravelCalculator
{
    private const string TheNumberOfReindeersMustBeLessThanOrEqualTo = "The number of reindeers must be less than or equal to 53";
    private const string TheNumberOfReindeersMustBeGreaterThan = "The number of reindeers must be greater than 0";
    private const int MaxReindeer = 53;

    public static ulong CalculateTotalDistance(int numberOfReindeers)
    {
        EnsureValidReindeerNumber(numberOfReindeers);

        return CalculateDistanceForReindeersMathPow(numberOfReindeers);
    }
    
    public static ulong CalculateDistanceForReindeersMathPow(int numberOfReindeers)
    {
        if (numberOfReindeers == 1) return 1;
        return (ulong)(Math.Pow(2, numberOfReindeers) - 1);
    }
    
    public static ulong CalculateTotalDistanceRecursively(int numberOfReindeers)
    {
        if (numberOfReindeers == 1) return 1;
        checked
        {
            return 2 * CalculateTotalDistanceRecursively(numberOfReindeers - 1) + 1;
        }
    }
    
    public static ulong CalculateTotalDistanceLoop(int numberOfReindeers)
    {
        if (numberOfReindeers == 1) return 1;
        checked
        {
            ulong result = 0;
            for (int i = numberOfReindeers; i > 0; i--)
            {
                result = 2 * result + 1;
            }
            return result;
        }
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