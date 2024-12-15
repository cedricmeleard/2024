namespace SantaChristmasList.Operations;

public class Sleigh : Dictionary<Child, string>
{
    public bool ContainsGiftFor(Child child) => ContainsKey(child);
    public string GetGiftFor(Child child) => this[child];
}