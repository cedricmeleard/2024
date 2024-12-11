using Xunit;

namespace Workshop.Tests;

public class SetupAWorkshop
{
    private readonly List<string> _toyNames = [];
    private Workshop _workshop;
    private Gift? _gift;
    public static SetupAWorkshop NewOne()
    {
        return new SetupAWorkshop();
    }
    
    public SetupAWorkshop WithAGiftNamed(string superNintendo)
    {
        _toyNames.Add(superNintendo);
        return this;
    }

    public SetupAWorkshop WhenActingOnAGiftWith(Func<Workshop, Gift?> func)
    {
        _workshop = Build();
        _gift = func(_workshop);
        
        return this;
    }
    public void ItShouldVerifyThat(Action<Gift?> action) => action(_gift);

    private Workshop Build()
    {
        var workshop = new Workshop();
        _toyNames.ForEach(toyName => workshop.AddGift(new Gift(toyName)));
        return workshop;
    }
}

public static class GiftExtension
{
    public static Gift? And(this Gift? gift) => gift;
    
    public static Gift? DoesExist(this Gift? gift)
    { 
        Assert.NotNull(gift);
        return gift;
    }
    
    public static Gift? IsProduced(this Gift? gift)
    {
        Assert.Equal(Status.Produced, gift?.Status);
        return gift;
    }
    
    public static void DoesNotExists(this Gift? gift) => Assert.Null(gift);
}