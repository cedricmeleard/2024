namespace Routine.Tests;

public class ReindeerFeederDouble : IReindeerFeeder
{
    public void FeedReindeers() => FeedReindeersWasCalled = true;
    public bool FeedReindeersWasCalled { get; private set; }

}