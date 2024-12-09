namespace GiftWish.Tests;

public class ChildBuilder
{
    private readonly string _firstName;
    private readonly string _lastName;
    private int _age;
    private readonly Behavior _behavior;
    private GiftRequestBuilder _giftRequest;

    private ChildBuilder(string firstName, string lastName, Behavior behavior)
    {
        _firstName = firstName;
        _lastName = lastName;
        _behavior = behavior;
    }

    public static ChildBuilder WithANiceChildNamed(string firstName, string lastName) 
        => new(firstName, lastName, Behavior.Nice);
    
    public static ChildBuilder WithANaughtyChildNamed(string firstName, string lastName) 
        => new(firstName, lastName, Behavior.Naughty);

    
    public ChildBuilder WithTheAgeOf(int age)
    {
        _age = age;
        return this;
    }

    public ChildBuilder WhoLikeToHave(string name)
    {
        _giftRequest = GiftRequestBuilder.CreateANiceToHaveGift(name);
        return this;
    }
    
    public ChildBuilder WhoDreamOf(string name)
    {
        _giftRequest = GiftRequestBuilder.CreateADreamGift(name);
        return this;
    }
    
    public Child Build() => new(
        _firstName, 
        _lastName, 
        _age, 
        _behavior, 
        _giftRequest.Build());

    public ChildBuilder ThatIsNotFeasible()
    {
        _giftRequest?.ThatIsNotFeasible();
        return this;
    }
}