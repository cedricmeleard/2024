using FakeItEasy;
using Moq;
using Times = Moq.Times;

namespace Routine.Tests
{
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
        public void StartRoutine_With_Moq()
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