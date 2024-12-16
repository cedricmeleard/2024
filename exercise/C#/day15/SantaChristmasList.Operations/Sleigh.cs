namespace SantaChristmasList.Operations;

public class Sleigh
{
    private readonly Dictionary<Child, string> _gifts = new();

    public string GetGiftFor(Child child)
        => _gifts.GetValueOrDefault(child, SleighMessages.ChildNotNiceThisYearMessage);

    public void AddGift(Child child, Gift gift)
        => _gifts.Add(child, string.Format(SleighMessages.GiftHasBeenLoaded, gift.Name));

    public void AddError(Child child, string errorMessage)
        => _gifts.Add(child, errorMessage);
}