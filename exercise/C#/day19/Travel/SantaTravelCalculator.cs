namespace Travel
{
public static class SantaTravelCalculator
{
    public static long CalculateTotalDistanceRecursively(int numberOfReindeers)
    {
        if (numberOfReindeers == 1) return 1;
        checked
        {
            return (long)(Math
                .Pow(2, numberOfReindeers) - 1);
        }
    }
}
}