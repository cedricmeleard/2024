namespace SantaChristmasList.Operations;

public class Business(Factory factory, Inventory inventory, WishList wishList)
{
    public Sleigh LoadGiftsInSleigh(params Child[] children)
    {
        var list = new Sleigh();
        foreach (var child in children)
        {
            var gift = wishList.IdentifyGift(child);
            if (gift is not null)
            {
                var manufactured = factory.FindManufacturedGift(gift);
                if (manufactured is not null)
                {
                    var finalGift = inventory.PickUpGift(manufactured.BarCode);
                    list.Add(child,
                        finalGift is not null
                            ? $"Gift: {finalGift.Name} has been loaded!"
                            : "Missing gift: The gift has probably been misplaced by the elves!");
                }
                else
                {
                    list.Add(child, "Missing gift: Gift wasn't manufactured!");
                }
            }
        }
        return list;
    }
}