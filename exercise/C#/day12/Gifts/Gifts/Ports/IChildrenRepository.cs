using Gifts.Domain;

namespace Gifts.Ports;

public interface IChildrenRepository
{
    Child? FindChild(string childName);

    void Add(Child child);
}