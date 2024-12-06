namespace ElfWorkshop;

public class ElfWorkshop
{
    // You can run into some problems here, a public get expose your List to the outside world, and it is possible to change it.
    // You can make it private and use a method or a property that return a readonly copy of a private field instead.
    public List<string> TaskList { get; } = [];

    // In C# 8.0 and later, explicitly mark nullable string parameters with 'string?' to clarify nullability.
    public void AddTask(string task)
    {
        // You can use string.IsNullOrEmpty, or even string.IsNullOrWhiteSpace here
        // by the way this fails silently if task is null or empty string, how can you handle this ?
        // You can use a Result<T> to return a Result<T> with an error message if the task is null or empty string.
        // But maybe it's a bit overkill here (it depends on specifications) 
        if (task != null && task != "")
        {
            // I prefer to use an early return and avoid nesting the actions in the if statement, what do you think of inverting the if statement here ?
            
            // Is it ok to add already existing task ?
            // If not you can use a HashSet to store the tasks and check if the task is already in the list (and add test accordingly)
            TaskList.Add(task);
        }
    }

    // I think that this method hide its real behavior, It doesn't not complete a task, it removes it from the list and returns it, FIFO queue
    // May be you can call it differently like Pop() ? or RemoveFirst() ? or CompleteFirst() ? ElfJobPopper()
    public string CompleteTask()
    {
        // you can invert the if statement here to avoid nesting  
        // By the way this usecase is not covered by the tests
        if (TaskList.Count > 0)
        {
            var task = TaskList[0];
            TaskList.RemoveAt(0);
            return task;
        }

        // I think that returning null or empty here is not a good idea,
        // You'll actually need not to forget an extra step to check that something actually happen in caller methods and returned task exists.
        // One good way to avoid this could be return a Result<string> that can be a Success or a Failure
        // Or more advanced an Option<string> that can be a Some or a None.
        return null;
    }
}