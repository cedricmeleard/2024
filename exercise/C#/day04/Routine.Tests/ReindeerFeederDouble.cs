namespace Routine.Tests;

public class ReindeerFeederDouble : IReindeerFeeder
{
    private bool _assertFeedReindeersWasCalled;
    public void FeedReindeers() => _assertFeedReindeersWasCalled = true;
    public void ShouldHaveFedAllReindeers() =>  _assertFeedReindeersWasCalled.Should().BeTrue();
}