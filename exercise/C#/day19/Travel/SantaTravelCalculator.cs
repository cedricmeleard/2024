namespace Travel
{
public static class SantaTravelCalculator
{
    public static ulong CalculateTotalDistanceRecursively(int numberOfReindeers)
        => (ulong)(Math.Pow(2, numberOfReindeers) - 1);
}
}