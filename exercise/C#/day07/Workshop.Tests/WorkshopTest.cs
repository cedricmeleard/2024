using Xunit;

namespace Workshop.Tests;

public class WorkshopTest
{
    private const string ToyName = "1 Super Nintendo";

    [Fact]
    public void CompletingAGiftShouldSetItsStatusToProduced() 
        => SetupAWorkshop.NewOne()
            .WithAGiftNamed(ToyName)
            .WhenActingOnAGiftWith(workshop => workshop.CompleteGift(ToyName))
            .ItShouldVerifyThat(completedGift 
                => completedGift.DoesExist().And().IsProduced());

    [Fact]
    public void CompletingANonExistingGiftShouldReturnNull()
        => SetupAWorkshop.NewOne()
            .WhenActingOnAGiftWith(workshop => workshop.CompleteGift("NonExistingToy"))
            .ItShouldVerifyThat(completedGift => completedGift.DoesNotExists());
}