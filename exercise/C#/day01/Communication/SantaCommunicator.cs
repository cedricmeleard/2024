using Communication.Domain;

namespace Communication;

public class SantaCommunicator(int numberOfDaysToRest)
{
    public string ComposeMessage(
        Reindeer reindeer,
        int numberOfDaysBeforeChristmas)
    {
        var daysBeforeReturn = DaysBeforeReturn(reindeer.NumbersOfDaysForComingBack, numberOfDaysBeforeChristmas);
        return
            $"Dear {reindeer.Name}, please return from {reindeer.CurrentLocation} in {daysBeforeReturn} day(s) to be ready and rest before Christmas.";
    }

    public bool IsOverdue(
        Reindeer reindeer,
        int numberOfDaysBeforeChristmas,
        ILogger logger)
    {
        if (DaysBeforeReturn(reindeer.NumbersOfDaysForComingBack, numberOfDaysBeforeChristmas) <= 0)
        {
            logger.Log($"Overdue for {reindeer.Name} located {reindeer.CurrentLocation}.");
            return true;
        }

        return false;
    }

    private int DaysBeforeReturn(int numbersOfDaysForComingBack, int numberOfDaysBeforeChristmas) =>
        numberOfDaysBeforeChristmas - numbersOfDaysForComingBack - numberOfDaysToRest;
}