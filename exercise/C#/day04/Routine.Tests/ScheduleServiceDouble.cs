using FluentAssertions;

namespace Routine.Tests;

public class ScheduleServiceDouble : IScheduleService
{
    public Schedule TodaySchedule()
    {
        Console.WriteLine("IScheduleService : TodaySchedule:");
        var schedule = new Schedule
        {
            Tasks = ["Make sure Bryan got his present", "Verify Donald matches conditions required for a present"]
        };
        return schedule;
    } 
    public void OrganizeMyDay(Schedule schedule) => Console.WriteLine($"IScheduleService : OrganizeMyDay with {schedule.Show()}");
    public void Continue() => Console.WriteLine("IScheduleService : Continue");
}

public static class ScheduleDoubleExtensions
{
    public static string Show(this Schedule schedule) => $"Schedule :\n {string.Join(',', schedule.Tasks)}";
}