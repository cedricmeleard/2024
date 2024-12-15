namespace SantaChristmasList.Operations;

public class Sleigh
{
    private readonly Dictionary<Child, string> _gifts = new();

    public string GetGiftFor(Child child)
    {
        return !_gifts.ContainsKey(child)
            ? SleighMessages.ChildNotNiceThisYearMessage
            : _gifts[child];
    }

    public void AddGift(Child child, Gift gift)
    {
        _gifts.Add(child, $"Gift: {gift.Name} has been loaded!");
    }

    public void AddMisplacedGift(Child child)
    {
        _gifts.Add(child, SleighMessages.GiftMisplacedByElvesMessage);
    }

    public void AddNotManufactured(Child child)
    {
        _gifts.Add(child, SleighMessages.GiftNotManufacturedMessage);
    }
}