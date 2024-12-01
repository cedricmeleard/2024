using Communication.Domain;

namespace Communication;

public class SantaCommunicator(int numberOfDaysToRest)
{
    private const string MessageForReindeerToReturn = "Dear {0}, please return from {1} in {2} day(s) to be ready and rest before Christmas.";
    private const string MessageForOverdue = "Overdue for {0} located {1}.";
    
    public string ComposeMessage(Reindeer reindeer, int numberOfDaysBeforeChristmas) 
        => string.Format(MessageForReindeerToReturn,
                reindeer.Name,
                reindeer.CurrentLocation,
                new DaysForReturn(reindeer, numberOfDaysToRest).AndDaysBeforeChristmas(numberOfDaysBeforeChristmas).Value);

    public bool IsOverdue(Reindeer reindeer, int numberOfDaysBeforeChristmas, ILogger logger)
    {
        if (new DaysForReturn(reindeer, numberOfDaysToRest)
            .AndDaysBeforeChristmas(numberOfDaysBeforeChristmas).IsEnough) {
            return false;
        }
        
        logger.Log(string.Format(MessageForOverdue, reindeer.Name, reindeer.CurrentLocation));
        return true;
    }
}