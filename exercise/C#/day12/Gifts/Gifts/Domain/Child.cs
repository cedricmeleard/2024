namespace Gifts.Domain;

public class Child(string name, Behavior behavior)
{
    private readonly WishList _wishList = new();
    public string Name { get; } = name;

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => _wishList.AddFirstWish(firstChoice).AddThen(secondChoice).AddThen(thirdChoice);

    public Toy? GetToy() => behavior switch
    {
        Behavior.Naughty => _wishList.GetAtIndex[^1],
        Behavior.Nice => _wishList.GetAtIndex[1],
        Behavior.VeryNice => _wishList.GetAtIndex[0],
        _ => null
    };
}

public enum Behavior
{
    VeryNice,
    Nice,
    Naughty
}