namespace Communication.Domain;

public class Location(string currentLocation, int numbersOfDaysForComingBack)
{
    public string CurrentLocation { get; } = currentLocation;
    public int NumbersOfDaysForComingBack { get; } = numbersOfDaysForComingBack;
}