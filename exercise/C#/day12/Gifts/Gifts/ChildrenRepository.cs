namespace Gifts;

public interface IChildrenRepository
{
    Child? FindChild(string childName);

    void Add(Child child);
}

public class ChildrenRepository : IChildrenRepository
{
    private readonly List<Child> _children = [];

    public Child? FindChild(string childName)
        => _children.FirstOrDefault(c => c.Name == childName);

    public void Add(Child child) => _children.Add(child);
}