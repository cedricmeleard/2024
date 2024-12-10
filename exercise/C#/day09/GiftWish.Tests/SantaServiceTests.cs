namespace GiftWish.Tests;

public class SantaServiceTests
{
    private readonly SantaService _service = new();

    [Fact]
    public void RequestIsApprovedForNiceChildWithFeasibleGift()
    {
        var requestOfANiceChild = Create
            .ANiceChild()
            .WhoDreamsOfAGift()
            .Build();
        
        _service.EvaluateRequest(requestOfANiceChild).ShouldBeApproved();
    }

    [Fact]
    public void RequestIsDeniedForNaughtyChild()
    {
        var requestOfNaughtyChild = Create
            .ANaughtyChild()
            .WhoLikeToHaveAGift()
            .Build();
        
        _service.EvaluateRequest(requestOfNaughtyChild).ShouldBeDenied();
    }

    [Fact]
    public void RequestIsDeniedForNiceChildWithInfeasibleGift()
    {
        var infeasibleGift = Create
            .ANiceChild()
            .WhoDreamsOfAGift().ThatIsNotFeasible()
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
    