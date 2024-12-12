namespace Gifts.Domain;

public class Child(string name, Behavior behavior)
{
    private readonly WishList _wishList = new();

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => _wishList.AddFirstWish(firstChoice).AddThen(secondChoice).AddThen(thirdChoice);

    public Toy? ChooseToy() => behavior switch
    {
        Behavior.Naughty => _wishList.Raw[^1],
        Behavior.Nice => _wishList.Raw[1],
        Behavior.VeryNice => _wishList.Raw[0],
        _ => null
    };

    public bool Is(string childName) => childName == name;
}

public enum Behavior
{
    VeryNice,
    Nice,
    Naughty
}