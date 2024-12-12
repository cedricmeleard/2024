using System.Collections.ObjectModel;

namespace Gifts;

public class WishList
{
    private readonly List<Toy> _wishList = [];
    public ReadOnlyCollection<Toy> Get => new(_wishList);

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