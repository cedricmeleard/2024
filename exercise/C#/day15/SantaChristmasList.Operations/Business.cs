namespace SantaChristmasList.Operations;

public class Business(Factory factory, Inventory inventory, WishList wishList)
{
    public Sleigh LoadGiftsInSleigh(params Child[] children)
    {
        var list = new Sleigh();
        foreach (var child in children)
        {
            wishList.IdentifyGift(child)
                .IfSome(
                    gift => factory
                        .FindManufacturedGift(gift)
                        .Match(manufactured
                                => inventory
                                    .PickUpGift(manufactured.BarCode)
                                    .Match(pickedGift => list.AddGift(child, pickedGift),
                                        () => list.AddMisplacedGift(child)),
                            () => list.AddNotManufactured(child)));
        }
        return list;
    }
}