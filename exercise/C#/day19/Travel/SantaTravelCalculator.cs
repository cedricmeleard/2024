namespace Travel
{
public static class SantaTravelCalculator
{
    public static long CalculateTotalDistanceRecursively(int numberOfReindeers)
        => (long)(Math.Pow(2, numberOfReindeers) - 1);
}
}