namespace SantaChristmasList.Operations;

public class Business(Factory factory, Inventory inventory, WishList wishList)
{
    public Sleigh LoadGiftsInSleigh(params Child[] children)
    {
        var inSleigh = new Sleigh();
        foreach (var child in children)
        {
            LoadGiftForChild(child, inSleigh);
        }
        return inSleigh;
    }

    private void LoadGiftForChild(Child child, Sleigh list)
        => wishList
            .IdentifyGift(child)
            .IfSome(gift => factory.FindManufacturedGift(gift)
                .Match(manufactured => LoadManufacturedGift(child, list, manufactured),
                    () => list.AddNotManufactured(child)));

    private void LoadManufacturedGift(Child child, Sleigh list, ManufacturedGift manufactured)
        => inventory
            .PickUpGift(manufactured.BarCode)
            .Match(pickedGift => list.AddGift(child, pickedGift),
                () => list.AddMisplacedGift(child));
}