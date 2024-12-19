namespace Travel
{
public static class SantaTravelCalculator
{
    public static int CalculateTotalDistanceRecursively(int numberOfReindeers)
    {
        if (numberOfReindeers == 1) return 1;
        checked
        {
            return (int)Math
                .Pow(2, numberOfReindeers) - 1;
        }
    }
}
}