using FakeItEasy;
using Xunit;

namespace Routine.Tests;

public class RoutineTests
{
    [Fact]
    public void StartRoutine_With_FakeItEasy()
    {
        // Arrange
        var emailService = A.Fake<IEmailService>();
        var scheduleService = A.Fake<IScheduleService>();
        var reindeerFeeder = A.Fake<IReindeerFeeder>();
            
        // Act
        new Routine(emailService, scheduleService, reindeerFeeder)
            .Start();
            
        // Assert
        A.CallTo(() => scheduleService.TodaySchedule()).MustHaveHappened();
        A.CallTo(() => scheduleService
            .OrganizeMyDay(A<Schedule>.That.IsInstanceOf(typeof(Schedule)))).MustHaveHappened();
        A.CallTo(() => scheduleService.Continue()).MustHaveHappened();
        A.CallTo(() => reindeerFeeder.FeedReindeers()).MustHaveHappened();
        A.CallTo(() => emailService.ReadNewEmails()).MustHaveHappened();
    }
     
    [Fact]
    public void StartRoutine_With_Manual_Test_Doubles()
    {
        // Arrange
        var emailService = new EmailServiceDouble();
        var scheduleService = new ScheduleServiceDouble();
        var reindeerFeeder = new ReindeerFeederDouble();
            
        // Act
        new Routine(emailService, scheduleService, reindeerFeeder)
            .Start();

        scheduleService.ShouldPrepareTodaySchedule();
        scheduleService.ShouldHaveOrganizedMyDay();
        reindeerFeeder.ShouldHaveFedAllReindeers();
        emailService.ShouldEnsureNewEmailHasBeenRead();
        scheduleService.SchedulingCanContinue();
    }
}