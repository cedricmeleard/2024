using FluentAssertions;
using FluentAssertions.Primitives;
using Xunit;

namespace GiftWish.Tests;

public class SantaServiceTests
{
    private readonly SantaService _service = new();

    [Fact]
    public void RequestIsApprovedForNiceChildWithFeasibleGift()
    {
        var requestOfANiceChild = ChildBuilder
            .WithANiceChildNamed()
            .WithAGiftHeDreamAbout()
            .Build();
        
        _service.EvaluateRequest(requestOfANiceChild).ShouldBeApproved();
    }

    [Fact]
    public void RequestIsDeniedForNaughtyChild()
    {
        var requestOfNaughtyChild = ChildBuilder
            .WithANaughtyChildNamed()
            .WithAGiftHeLikeToHave()
            .Build();
        
        _service.EvaluateRequest(requestOfNaughtyChild).ShouldBeDenied();
    }

    [Fact]
    public void RequestIsDeniedForNiceChildWithInfeasibleGift()
    {
        var infeasibleGift = ChildBuilder
            .WithANiceChildNamed()
            .WithAGiftHeDreamAbout().ThatIsNotFeasible()
            .Build();
        
        _service.EvaluateRequest(infeasibleGift).ShouldBeDenied();
    }
}

public static class TestExtensions
{
    public static void ShouldBeDenied(this bool result) 
        => result.Should().BeFalse();

    public static void ShouldBeApproved(this bool result) 
        => result.Should().BeTrue();
}
    