namespace GiftWish.Tests;

public class GiftRequestBuilder
{
    private bool _isFeasible = true;
    private readonly string _name;
    private readonly Priority _priority;

    private GiftRequestBuilder(string name, Priority niceToHave)
    {
        _name = name;
        _priority = niceToHave;
    }

    public GiftRequestBuilder ThatIsNotFeasible()
    {
        _isFeasible = false;
        return this;
    }

    public static GiftRequestBuilder CreateADreamGift(string name)
        => new GiftRequestBuilder(name, Priority.Dream);
    
    public static GiftRequestBuilder CreateANiceToHaveGift(string name)
        => new GiftRequestBuilder(name, Priority.NiceToHave);

    public GiftRequest Build()
    { 
        return new GiftRequest(_name, _isFeasible, _priority);
    }
}