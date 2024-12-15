namespace SantaChristmasList.Operations;

public class Sleigh : Dictionary<Child, string>
{
    public string GetGiftFor(Child child)
    {
        return !ContainsKey(child)
            ? "Missing gift: Child wasn't nice this year!"
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

public static class SleighMessages
{
    public const string ChildNotNiceThisYearMessage = "Missing gift: Child wasn't nice this year!";
    public const string GiftMisplacedByElvesMessage = "Missing gift: The gift has probably been misplaced by the elves!";
    public const string GiftNotManufacturedMessage = "Missing gift: Gift wasn't manufactured!";
}