using FluentAssertions;
using Xunit;

namespace TaskAssignmentSystem.Tests;

public class TaskAssignmentTests
{
    private readonly TaskAssignmentService _system;
    private readonly List<Elf?> _elves =
    [
        new(1, 5),
        new(2, 10),
        new(3, 20)
    ];
    public TaskAssignmentTests()
    {
        
        _system = new TaskAssignmentService(_elves);
    }

    [Fact]
    public void ReassignTask_ShouldNotChangeElfSkillLevels()
    {
        // Act
        var success = _system.TryReassignTask(1, 2);

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
        // Act
        _system.ResetAllSkillsToBaseline(10);

        // Assert
        foreach (var elf in _elves)
        {
            elf.SkillLevel.Should().Be(10);
        }
    }
    
    [Fact]
    public void DecreaseSkillLevel_ShouldNotReduceSkillBelowOne()
    {
        // Act
        _elves.Find(x => x.Id == 1)?
            .DecreaseSkill(10); // Excessive decrement

        // Assert
        var alice = _elves.First(e => e.Id == 1);
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
        var highestSkillElf = _system.GetElfWithHighestSkill();
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
        _elves.Find(x=>x.Id == 1)?
            .IncreaseSkill(3);
        var elf = _system.AssignTask(7);
        elf.Id.Should().Be(1);
    }

    [Fact]
    public void DecreaseSkillLevel_UpdatesElfSkillCorrectly()
    {
        _elves.Find(x=>x.Id == 1)?.DecreaseSkill(3);
        _elves.Find(x=>x.Id == 2)?.DecreaseSkill(5);

        var elf = _system.AssignTask(4);
        elf.Id.Should().Be(2);
        elf.SkillLevel.Should().Be(5);
    }

    [Fact]
    public void ReassignTask_ToAnElf_WithSufficientSkills_ChangesAssignmentCorrectly()
    {
        var result = _system.TryReassignTask(1, 3);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void ReassignTask_ToAnElf_WithInSufficientSkills_DoesNotChangesAssignment()
    {
        var result = _system.TryReassignTask(3, 1);
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
        var sortedElves = _system.GetElvesBySkillDescending();
        sortedElves.ConvertAll(elf => elf.Id).Should().Equal(new List<int> {3, 2, 1});
    }

    [Fact]
    public void ResetAllSkillsToBaseline_ResetsAllElvesSkillsToSpecifiedBaseline()
    {
        _system.ResetAllSkillsToBaseline(10);
        var elves = _system.GetElvesBySkillDescending();
        foreach (var elf in elves)
        {
            elf.SkillLevel.Should().Be(10);
        }
    }

    [Fact]
    public void DecreaseSkillLevel_ThenFindSuitableElf()
    {
        _elves.First(e => e.Id == 1).DecreaseSkill(10);
        var elf = _system.AssignTask(4);
        elf.Id.Should().Be(2);
        elf.SkillLevel.Should().Be(10);
    }
    
    [Fact]
    public void TaskAssignmentService_ShouldThrowException_WhenElvesListIsNull()
    {
        Action act = () => new TaskAssignmentService(null);

        act.Should().Throw<ArgumentException>().WithMessage("A list of elves is required.");
    }

    [Fact]
    public void TaskAssignmentService_ShouldThrowException_WhenElvesListIsEmpty()
    {
        Action act = () => new TaskAssignmentService(new List<Elf?>());

        act.Should().Throw<ArgumentException>().WithMessage("A list of elves is required.");
    }
    
    [Fact]
    public void AssignTask_ShouldReturnElf_WhenExactSkillLevelMatches()
    {
        var elves = new List<Elf?>
        {
            new Elf(1, 5),
            new Elf(2, 8)
        };

        var service = new TaskAssignmentService(elves);
        var assignedElf = service.AssignTask(5);

        assignedElf.Id.Should().Be(2); // Elf 2 is the one whose skill level (8) meets criteria
    }

    [Fact]
    public void AssignTask_ShouldReturnNull_WhenNoMatchingElf()
    {
        var elves = new List<Elf?>
        {
            new Elf(1, 5),
            new Elf(2, 8)
        };

        var service = new TaskAssignmentService(elves);
        var assignedElf = service.AssignTask(10); // No elf can handle this skill level

        assignedElf.Should().BeNull();
    }
    
    [Fact]
    public void ReassignTask_ShouldReturnFalse_WhenFromElfSkillExceedsToElf()
    {
        var elves = new List<Elf?>
        {
            new Elf(1, 10),
            new Elf(2, 5)
        };

        var service = new TaskAssignmentService(elves);
        var result = service.TryReassignTask(1, 2); // FromElf (10) > ToElf(5)

        result.Should().BeFalse();
    }

    [Fact]
    public void ReassignTask_ShouldReturnFalse_WhenEitherElfDoesNotExist()
    {
        var elves = new List<Elf?>
        {
            new Elf(1, 10),
            new Elf(2, 5)
        };

        var service = new TaskAssignmentService(elves);
        var result = service.TryReassignTask(1, 99); // ID 99 does not exist

        result.Should().BeFalse();
    }
    
    [Fact]
    public void ResetAllSkillsToBaseline_ShouldSetSkillLevelsToBaseline()
    {
        var elves = new List<Elf?>
        {
            new Elf(1, 10),
            new Elf(2, 15)
        };

        var service = new TaskAssignmentService(elves);
        service.ResetAllSkillsToBaseline(5);

        elves.All(e => e.SkillLevel == 5).Should().BeTrue();
    }

    [Fact]
    public void ResetAllSkillsToBaseline_ShouldThrowException_WhenBaselineIsInvalid()
    {
        var elves = new List<Elf?>
        {
            new Elf(1, 10),
            new Elf(2, 15)
        };

        var service = new TaskAssignmentService(elves);
        Action act = () => service.ResetAllSkillsToBaseline(0); // Invalid baseline

        act.Should().Throw<ArgumentException>().WithMessage("Baseline must be positive.");
    }
}