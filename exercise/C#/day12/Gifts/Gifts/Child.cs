namespace Gifts;

public class Child(string name, Behavior behavior)
{
    public string Name { get; } = name;
    public Behavior Behavior { get; } = behavior;

    public WishList WishList { get; } = new();

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => WishList.AddFirstWish(firstChoice).AddThen(secondChoice).AddThen(thirdChoice);
}

public enum Behavior
{
    VeryNice,
    Nice,
    Naughty
}