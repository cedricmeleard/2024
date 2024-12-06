using FluentAssertions;
using Xunit;

// I suggest you to use the file scoped namespace in general to reduce nesting and be consistant with the rest of the project.
namespace ElfWorkshop.Tests
{
    public class ElfWorkshopTests
    {
        [Fact]
        // Maybe the name is a little to trivial, what about something like A_New_Task_Added_To_ElfWorkshop_Should_Be_Present_In_TaskList ?
        // Also I Propose removing the name of the method "AddTask" in you tests because it makes it a lot to change if you decide to rename the method in ElfWorkshop class. 
        public void AddTask_Should_Add_Task()
        {
            // it's a just a suggestion, but, in general it could be a good idea to use comments
            // Arrange, Act, Assert to make it easier to understand what you are doing
            var workshop = new ElfWorkshop();
            workshop.AddTask("Build toy train");
            workshop.TaskList.Should().Contain("Build toy train");
        }

        [Fact]
        // This test is pretty much the same as "AddTask_Should_Add_Task", maybe you can Transform it to a theory of Inlined Data for "Build toy train", "Craft dollhouse" and "Paint bicycle"
        // And merge the 3 first Test AddTask_Should_Add_Task, AddTask_Should_Add_Craft_Dollhouse_Task and AddTask_Should_Add_Paint_Bicycle_Task into it   
        public void AddTask_Should_Add_Craft_Dollhouse_Task()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask("Craft dollhouse");
            workshop.TaskList.Should().Contain("Craft dollhouse");
        }

        [Fact]
        // Can be removed as it doesn't bring really more than the firsts tests.
        // Or what about testing that when you already have a Task, it adds one more instead ?  
        // You can also look to property based testing here to ensure adding any kind of tast is working correctly.
        public void AddTask_Should_Add_Paint_Bicycle_Task()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask("Paint bicycle");
            workshop.TaskList.Should().Contain("Paint bicycle");
        }

        [Fact]
        // This test suggest that AddTask can fail silently,
        // user can think it's ok to pass an empty string but it's not
        public void AddTask_Should_Handle_Empty_Tasks_Correctly()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask("");
            workshop.TaskList.Should().BeEmpty();
        }
        
        [Fact]
        // You should use a theory for "" and null. And maybe Test the same for " "
        // One good practice I learned recently is to separate Valid case from Invalid case,
        // you invalid test case could be in a nested class called Failure or Invalid, and will be way easier to read.
        public void AddTask_Should_Handle_Null_Tasks_Correctly()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask(null);
            workshop.TaskList.Should().BeEmpty();
        }

        [Fact]
        public void CompleteTask_Should_Remove_Task()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask("Wrap gifts");
            var removedTask = workshop.CompleteTask();
            removedTask.Should().Be("Wrap gifts");
            workshop.TaskList.Should().BeEmpty();
            // Maybe you can add a test to check what happens when you complete a task on ElfWorkshop with no task ?
        }
    }
}