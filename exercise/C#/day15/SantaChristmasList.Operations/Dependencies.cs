namespace SantaChristmasList.Operations;

public class Factory : Dictionary<Gift, ManufacturedGift>
{
    public Option<ManufacturedGift> FindManufacturedGift(Gift gift)
    {
        return ContainsKey(gift) ? this[gift] : Option<ManufacturedGift>.None;
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
    public Option<Gift> IdentifyGift(Child child)
    {
        return ContainsKey(child) ? this[child] : Option<Gift>.None;
    }
}