using FluentAssertions;
using Xunit;

namespace GiftWish.Tests;

public class SantaServiceTests
{
    private readonly SantaService _service = new();

    [Fact]
    public void RequestIsApprovedForNiceChildWithFeasibleGift()
    {
        var niceChild = ChildBuilder
            .CreateANiceChildNamed("Alice", "Thomas")
            .WithTheAgeOf(9)
            .WithAGiftRequest(GiftRequestBuilder.CreateANiceToHaveGift("Bicycle"))
            .Build();
        
        _service.EvaluateRequest(niceChild).Should().BeTrue();
    }

    [Fact]
    public void RequestIsDeniedForNaughtyChild()
    {
        var naughtyChild = ChildBuilder
            .CreateANaughtyChildNamed("Noa", "Thierry")
            .WithTheAgeOf(6)
            .WithAGiftRequest(GiftRequestBuilder.CreateADreamGift("SomeToy"))
            .Build();
        
        _service.EvaluateRequest(naughtyChild).Should().BeFalse();
    }

    [Fact]
    public void RequestIsDeniedForNiceChildWithInfeasibleGift()
    {
        var niceChildWithInfeasibleGift = ChildBuilder
            .CreateANiceChildNamed("Charlie", "Joie")
            .WithTheAgeOf(3)
            .WithAGiftRequest(
                GiftRequestBuilder.CreateADreamGift("AnotherToy").ThatIsNotFeasible())
            .Build();
        
        _service.EvaluateRequest(niceChildWithInfeasibleGift).Should().BeFalse();
    }
}