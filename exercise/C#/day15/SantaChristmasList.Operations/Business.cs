namespace SantaChristmasList.Operations;

public class Business(Factory factory, Inventory inventory, WishList wishList)
{
    public Sleigh LoadGiftsInSleigh(params Child[] children)
    {
        var inSleigh = new Sleigh();
        foreach (var child in children)
        {
            wishList
                .IdentifyGift(child)
                .Bind(gift => factory.FindManufacturedGift(gift))
                .Bind(manufactured => inventory.PickUpGift(manufactured.BarCode))
                .Match(
                    gift => inSleigh.AddGift(child, gift),
                    error => inSleigh.AddError(child, error.Message));
        }
        return inSleigh;
    }
}