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

    public static GiftRequestBuilder CreateADreamGift(string name) 
        => new(name, Priority.Dream);
    
    public static GiftRequestBuilder CreateANiceToHaveGift(string name) 
        => new(name, Priority.NiceToHave);
    
    public void ThatIsNotFeasible() => _isFeasible = false;
    
    public GiftRequest Build() => new(_name, _isFeasible, _priority);
}