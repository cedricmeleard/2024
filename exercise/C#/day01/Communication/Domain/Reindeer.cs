namespace Communication.Domain;

public class Reindeer(ReindeerName name, Location location)
{
    public ReindeerName Name { get; } = name;
    public string CurrentLocation => location.CurrentLocation;
    public int NumbersOfDaysForComingBack => location.NumbersOfDaysForComingBack;
}