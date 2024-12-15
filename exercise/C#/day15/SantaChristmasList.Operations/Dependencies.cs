namespace SantaChristmasList.Operations;

public class Factory : Dictionary<Gift, ManufacturedGift>
{
    public ManufacturedGift FindManufacturedGift(Gift gift)
    {
        return ContainsKey(gift) ? this[gift] : null;
    }
}

public class Inventory : Dictionary<string, Gift>
{
    public Option<Gift> PickUpGift(string barCode)
    {
        return ContainsKey(barCode) ? this[barCode] : Option<Gift>.None;
    }
}

public class WishList : Dictionary<Child, Gift>
{
    public Gift IdentifyGift(Child child)
    {
        return ContainsKey(child) ? this[child] : null;
    }
}