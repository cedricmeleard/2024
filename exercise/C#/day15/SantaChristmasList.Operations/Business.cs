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
                    inventory
                        .PickUpGift(manufactured.BarCode)
                        .Match(
                            pickedGift => list.AddGift(child, pickedGift),
                            () => list.AddMisplacedGift(child));
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