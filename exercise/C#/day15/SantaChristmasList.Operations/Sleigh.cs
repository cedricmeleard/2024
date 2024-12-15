namespace SantaChristmasList.Operations;

public class Sleigh : Dictionary<Child, string>
{
    public string GetGiftFor(Child child)
    {
        return !ContainsKey(child)
            ? SleighMessages.ChildNotNiceThisYearMessage
            : this[child];
    }

    public void AddGift(Child child, Gift gift)
    {
        Add(child, $"Gift: {gift.Name} has been loaded!");
    }

    public void AddMisplacedGift(Child child)
    {
        Add(child, SleighMessages.GiftMisplacedByElvesMessage);
    }

    public void AddNotManufactured(Child child)
    {
        Add(child, SleighMessages.GiftNotManufacturedMessage);
    }
}