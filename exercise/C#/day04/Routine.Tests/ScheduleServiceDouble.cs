namespace Routine.Tests;

public class ScheduleServiceDouble : IScheduleService
{
    private bool _assertTodayScheduleWasCalled;
    private Schedule? _schedule;
    private bool _assertContinueWasCalled;
    public Schedule TodaySchedule()
    {
        _assertTodayScheduleWasCalled = true;
        return new Schedule {
            Tasks = [
                "Make sure Bryan got his present", 
                "Verify Donald matches conditions required for a present"
            ]
        };
    }
    public void OrganizeMyDay(Schedule schedule) => _schedule = schedule;
    public void Continue() => _assertContinueWasCalled = true;
    public void ShouldPrepareTodaySchedule() => _assertTodayScheduleWasCalled.Should().BeTrue();
    public void ShouldHaveOrganizedMyDay() => (_schedule?.Tasks.Count == 2).Should().BeTrue();
    public void SchedulingCanContinue() => _assertContinueWasCalled.Should().BeTrue();
}