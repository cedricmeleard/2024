using Communication.Domain;

namespace Communication;

public class SantaCommunicator(int numberOfDaysToRest)
{
    public string ComposeMessage(
        ReindeerName reindeerName,
        Location location,
        int numberOfDaysBeforeChristmas)
    {
        var daysBeforeReturn = DaysBeforeReturn(location.NumbersOfDaysForComingBack, numberOfDaysBeforeChristmas);
        return
            $"Dear {reindeerName}, please return from {location.CurrentLocation} in {daysBeforeReturn} day(s) to be ready and rest before Christmas.";
    }

    public bool IsOverdue(
        ReindeerName reindeerName,
        Location location,
        int numberOfDaysBeforeChristmas,
        ILogger logger)
    {
        if (DaysBeforeReturn(location.NumbersOfDaysForComingBack, numberOfDaysBeforeChristmas) <= 0)
        {
            logger.Log($"Overdue for {reindeerName} located {location.CurrentLocation}.");
            return true;
        }

        return false;
    }

    private int DaysBeforeReturn(int numbersOfDaysForComingBack, int numberOfDaysBeforeChristmas) =>
        numberOfDaysBeforeChristmas - numbersOfDaysForComingBack - numberOfDaysToRest;
}