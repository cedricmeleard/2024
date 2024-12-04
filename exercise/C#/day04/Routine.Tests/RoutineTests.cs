using Moq;
using VerifyXunit;

namespace Routine.Tests
{
    public class RoutineTests
    {
        [Fact]
        public void StartRoutine_With_FakeItEasy()
        {
            // Arrange
            var emailService = new Mock<IEmailService>();
            var scheduleService = new Mock<IScheduleService>();
            var reindeerFeeder = new Mock<IReindeerFeeder>();
            
            // Act
            new Routine(emailService.Object, scheduleService.Object, reindeerFeeder.Object)
                .Start();
            
            // Assert
            scheduleService.Verify(x => x.TodaySchedule(), Times.Once);
            scheduleService.Verify(x => x.OrganizeMyDay(It.IsAny<Schedule>()), Times.Once);
            scheduleService.Verify(x => x.Continue(), Times.Once);
            
            reindeerFeeder.Verify(x => x.FeedReindeers(), Times.Once);
            
            emailService.Verify(x => x.ReadNewEmails(), Times.Once);
        }

        [Fact]
        public async Task StartRoutine_With_Manual_Test_Doubles()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);
            
            // Arrange
            var emailService = new EmailServiceDouble();
            var scheduleService = new ScheduleServiceDouble();
            var reindeerFeeder = new ReindeerFeederDouble();
            
            // Act
            new Routine(emailService, scheduleService, reindeerFeeder)
                .Start();

            await Verify(writer.ToString());
        }
    }
}