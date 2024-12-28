using LanguageExt.Common;

namespace SantaChristmasList.Operations;

public class Factory : Dictionary<Gift, ManufacturedGift>
{
    public Either<Error, ManufacturedGift> FindManufacturedGift(Gift gift)
        => ContainsKey(gift) ? this[gift] : Error.New(SleighMessages.GiftNotManufacturedMessage);
}

public class Inventory : Dictionary<string, Gift>
{
    public Either<Error, Gift> PickUpGift(string barCode)
        => ContainsKey(barCode) ? this[barCode] : Error.New(SleighMessages.GiftMisplacedByElvesMessage);
}

public class WishList : Dictionary<Child, Gift>
{
    public Either<Error, Gift> IdentifyGift(Child child)
        => ContainsKey(child) ? this[child] : Error.New(SleighMessages.ChildNotNiceThisYearMessage);
}