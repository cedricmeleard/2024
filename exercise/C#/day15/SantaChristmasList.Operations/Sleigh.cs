namespace SantaChristmasList.Operations;

public class Sleigh : Dictionary<Child, string>
{
    public string GetGiftFor(Child child)
    {
        return !ContainsKey(child)
            ? "Missing gift: Child wasn't nice this year!"
            : this[child];
    }
}