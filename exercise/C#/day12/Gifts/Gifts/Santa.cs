using Gifts.Domain;
using Gifts.Ports;

namespace Gifts;

public class Santa(IChildrenRepository childrenRepository)
{
    public Toy? ChooseToyForChild(string childName)
    {
        var found = childrenRepository.FindChild(childName);

        if (found == null)
            throw new InvalidOperationException("No such child found");

        return found.ChooseToy();
    }

    public void AddChild(Child child) =>
        // Actual implementation implies unique child names
        // otherwise when we try to get the child by name, we always get the first one
        childrenRepository.Add(child);
}