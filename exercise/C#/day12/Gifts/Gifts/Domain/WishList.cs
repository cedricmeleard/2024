using System.Collections.ObjectModel;

namespace Gifts.Domain;

public class WishList
{
    private readonly List<Toy> _wishList = [];
    public ReadOnlyCollection<Toy> Raw => new(_wishList);

    public WishList AddFirstWish(Toy toy)
    {
        _wishList.Add(toy);
        return this;
    }

    public WishList AddThen(Toy toy)
    {
        _wishList.Add(toy);
        return this;
    }
}