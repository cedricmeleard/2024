using Gifts.Domain;
using Gifts.Ports;

namespace Gifts;

public class Santa(IChildrenRepository childrenRepository)
{
    public Toy? ChooseToyForChild(string childName)
        => childrenRepository
               .FindChild(childName)?
               .ChooseToy()
           ?? throw new InvalidOperationException("No such child found");

    public void AddChild(Child child) =>
        // Actual implementation implies unique child names
        // otherwise when we try to get the child by name, we always get the first one
        childrenRepository.Add(child);
}