using Gifts.Domain;
using Gifts.Ports;

namespace Gifts.Adapters;

public class ChildrenRepository : IChildrenRepository
{
    private readonly List<Child> _children = [];

    public Child? FindChild(string childName)
        => _children.FirstOrDefault(c => c.Is(childName));

    public void Add(Child child) => _children.Add(child);
}