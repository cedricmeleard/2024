using Bogus;

namespace GiftWish.Tests;

public class Create
{
    private const int AgeWhereStillAChild = 77;
    private readonly string _firstName;
    private readonly string _lastName;
    private int _age;
    private readonly Behavior _behavior;
    private GiftRequestBuilder _giftRequest;

    private Create(Behavior behavior)
    {
        _firstName = new Faker().Name.FirstName();
        _lastName = new Faker().Name.LastName();
        _behavior = behavior;
        
        _age = new Faker().Random.Int(1, AgeWhereStillAChild);
    }

    public static Create ANiceChild() 
        => new(Behavior.Nice);
    
    public static Create ANaughtyChild() 
        => new(Behavior.Naughty);

    
    public Create WhoLikeToHaveAGift()
    {
        _giftRequest = GiftRequestBuilder.CreateANiceToHaveGift();
        return this;
    }
    
    public Create WhoDreamsOfAGift()
    {
        _giftRequest = GiftRequestBuilder.CreateADreamGift();
        return this;
    }
    
    public Child Build() => new(
        _firstName, 
        _lastName, 
        _age, 
        _behavior, 
        _giftRequest.Build());

    public Create ThatIsNotFeasible()
    {
        _giftRequest?.ThatIsNotFeasible();
        return this;
    }
}