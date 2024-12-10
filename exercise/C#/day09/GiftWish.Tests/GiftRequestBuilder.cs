using Bogus;

namespace GiftWish.Tests;

public class GiftRequestBuilder
{
    private bool _isFeasible = true;
    private readonly string _name;
    private readonly Priority _priority;

    private GiftRequestBuilder(Priority niceToHave)
    {
        _name = new Faker().Commerce.ProductName();
        _priority = niceToHave;
    }

    public static GiftRequestBuilder CreateADreamGift() 
        => new(Priority.Dream);
    
    public static GiftRequestBuilder CreateANiceToHaveGift() 
        => new(Priority.NiceToHave);
    
    public void ThatIsNotFeasible() => _isFeasible = false;
    
    public GiftRequest Build() => new(_name, _isFeasible, _priority);
}