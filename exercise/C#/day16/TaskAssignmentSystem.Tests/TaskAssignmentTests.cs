using FluentAssertions;
using Xunit;

namespace TaskAssignmentSystem.Tests;

public class TaskAssignmentTests
{
    private readonly TaskAssignment _system;
    private readonly List<Elf> _elves =
    [
        new(1, 5),
        new(2, 10),
        new(3, 20)
    ];
    public TaskAssignmentTests()
    {
        
        _system = new TaskAssignment(_elves);
    }

    [Fact]
    public void ReassignTask_ShouldNotChangeElfSkillLevels()
    {
        // Act
        var success = _system.ReassignTask(1, 2);

        // Assert
        success.Should().BeTrue();
        var alice = _elves.First(e => e.Id == 1);
        var bob = _elves.First(e => e.Id == 2);
        alice.SkillLevel.Should().Be(5); 
        bob.SkillLevel.Should().Be(10); 
    }
    
    [Fact]
    public void ResetAllSkillsToBaseline_ShouldSetAllElfSkillsToBaseline()
    {
        // Arrange
        var elves = new List<Elf>
        {
            new(1, 3),  // Alice
            new(2, 5),  // Bob
            new(3, 7)   // Charlie
        };
        var system = new TaskAssignment(elves);

        // Act
        system.ResetAllSkillsToBaseline(10);

        // Assert
        foreach (var elf in elves)
        {
            elf.SkillLevel.Should().Be(10);
        }
    }
    
    [Fact]
    public void AssignTaskBasedOnAvailability_ShouldBeDeterministic()
    {
        // Arrange
        var elves = new List<Elf>
        {
            new(1, 3),  // Alice
            new(2, 5),  // Bob
            new(3, 7)   // Charlie
        };
        var system = new TaskAssignment(elves);

        // Act
        var assignedElf1 = system.AssignTaskBasedOnAvailability(4);
        var assignedElf2 = system.AssignTaskBasedOnAvailability(4);

        // Assert: Both assignments should be consistent
        assignedElf1.Should().NotBeNull();
        assignedElf1.Should().BeEquivalentTo(assignedElf2);
    }
    
    [Fact]
    public void DecreaseSkillLevel_ShouldNotReduceSkillBelowOne()
    {
        // Arrange
        var elves = new List<Elf>
        {
            new(1, 3),  // Alice
            new(2, 5),  // Bob
        };
        var system = new TaskAssignment(elves);

        // Act
        system.DecreaseSkillLevel(1, 10); // Excessive decrement

        // Assert
        var alice = elves.First(e => e.Id == 1);
        alice.SkillLevel.Should().Be(1); // Skill should not go below 1
    }
    
    [Fact]
    public void AssignTask_ShouldAssignToAnyQualifiedElf()
    {
        // Act
        var assignedElf = _system.AssignTask(4);

        // Assert: Either Alice or Bob can be assigned
        new List<int> { 1, 2 }.Should().Contain(assignedElf.Id);
    }
    
    [Fact]
    public void ReportTaskCompletion_IncreasesTotalTasksCompleted()
    {
        _system.ReportTaskCompletion(1).Should().BeTrue();
        _system.TotalTasksCompleted.Should().Be(1);
    }

    [Fact]
    public void GetElfWithHighestSkill_ReturnsCorrectElf()
    {
        var highestSkillElf = _system.ElfWithHighestSkill();
        highestSkillElf.Id.Should().Be(3);
    }

    [Fact]
    public void AssignTask_AssignsElfBasedOnSkillLevel()
    {
        _system.AssignTask(8).Should().BeEquivalentTo(new Elf(2, 10));
    }

    [Fact]
    public void IncreaseSkillLevel_UpdatesElfSkillCorrectly()
    {
        _system.IncreaseSkillLevel(1, 3);
        var elf = _system.AssignTask(7);
        elf.Id.Should().Be(1);
    }

    [Fact]
    public void DecreaseSkillLevel_UpdatesElfSkillCorrectly()
    {
        _system.DecreaseSkillLevel(1, 3);
        _system.DecreaseSkillLevel(2, 5);

        var elf = _system.AssignTask(4);
        elf.Id.Should().Be(2);
        elf.SkillLevel.Should().Be(5);
    }

    [Fact]
    public void AssignTaskBasedOnAvailability_AssignsAvailableElf()
    {
        var elf = _system.AssignTaskBasedOnAvailability(10);
        elf.Should().NotBeNull();
    }

    [Fact]
    public void ReassignTask_ToAnElf_WithSufficientSkills_ChangesAssignmentCorrectly()
    {
        var result = _system.ReassignTask(1, 3);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void ReassignTask_ToAnElf_WithInSufficientSkills_DoesNotChangesAssignment()
    {
        var result = _system.ReassignTask(3, 1);
        result.Should().BeFalse();
    }

    [Fact]
    public void AssignTask_FailsWhenSkillsRequiredIsTooHigh()
    {
        _system.AssignTask(50).Should().BeNull();
    }

    [Fact]
    public void ListElvesBySkillDescending_ReturnsElvesInCorrectOrder()
    {
        var sortedElves = _system.ElvesBySkillDescending();
        sortedElves.ConvertAll(elf => elf.Id).Should().Equal(new List<int> {3, 2, 1});
    }

    [Fact]
    public void ResetAllSkillsToBaseline_ResetsAllElvesSkillsToSpecifiedBaseline()
    {
        _system.ResetAllSkillsToBaseline(10);
        var elves = _system.ElvesBySkillDescending();
        foreach (var elf in elves)
        {
            elf.SkillLevel.Should().Be(10);
        }
    }

    [Fact]
    public void DecreaseSkillLevel_UpdatesElfSkillAndDoesNotAllowNegativeValues()
    {
        _system.DecreaseSkillLevel(1, 10);
        var elf = _system.AssignTask(4);
        elf.Id.Should().Be(1);
        elf.SkillLevel.Should().Be(5);
    }
}