namespace Routine.Tests;

public class ScheduleServiceDouble : IScheduleService
{
    public Schedule TodaySchedule()
    {
        TodayScheduleWasCalled = true;
        return new Schedule {
            Tasks = [
                "Make sure Bryan got his present", 
                "Verify Donald matches conditions required for a present"
            ]
        };
    }
    public void OrganizeMyDay(Schedule schedule) => OrganizeMyDayWasCalledWithASchedule = schedule?.Tasks.Count == 2;
    public void Continue() => ContinueWasCalled = true;
    public bool TodayScheduleWasCalled { get; private set; }
    public bool OrganizeMyDayWasCalledWithASchedule { get; private set; }
    public bool ContinueWasCalled { get; private set; }
}