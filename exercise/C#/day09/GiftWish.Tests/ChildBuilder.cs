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

    public static ChildBuilder CreateANiceChildNamed(string firstName, string lastName) 
        => new(firstName, lastName, Behavior.Nice);
    
    public static ChildBuilder CreateANaughtyChildNamed(string firstName, string lastName) 
        => new(firstName, lastName, Behavior.Naughty);

    
    public ChildBuilder WithTheAgeOf(int age)
    {
        _age = age;
        return this;
    }

    public ChildBuilder WithAGiftRequest(GiftRequestBuilder giftRequest)
    {
        _giftRequest = giftRequest;
        return this;
    }
    
    public Child Build() => new(
        _firstName, 
        _lastName, 
        _age, 
        _behavior, 
        _giftRequest.Build());
}