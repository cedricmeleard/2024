using Communication.Domain;

namespace Communication;

internal class DaysForReturn(Reindeer reindeer, int numberOfDaysToRest)
{
    private int _numberOfDaysBeforeChristmas = 0;
    public DaysForReturn AndDaysBeforeChristmas(int numberOfDaysBeforeChristmas)
    {
        _numberOfDaysBeforeChristmas = numberOfDaysBeforeChristmas;
        return this;
    } 
    public int Value => _numberOfDaysBeforeChristmas - reindeer.NumbersOfDaysForComingBack - numberOfDaysToRest;
    public bool IsEnough => Value >= 0;
}