namespace SantaChristmasList.Operations;

public class Sleigh : Dictionary<Child, string>
{
    public string GetGiftFor(Child child)
    {
        return !ContainsKey(child)
            ? null
            : this[child];
    }
}