namespace Gifts;

public class Santa(IChildrenRepository childrenRepository)
{
    public Toy? ChooseToyForChild(string childName)
    {
        var found = childrenRepository.FindChild(childName);

        if (found == null)
            throw new InvalidOperationException("No such child found");

        return found.Behavior switch
        {
            Behavior.Naughty => found.WishList.Get[^1],
            Behavior.Nice => found.WishList.Get[1],
            Behavior.VeryNice => found.WishList.Get[0],
            _ => null
        };
    }

    public void AddChild(Child child) =>
        // Actual implementation implies unique child names
        // otherwise when we try to get the child by name, we always get the first one
        childrenRepository.Add(child);
}