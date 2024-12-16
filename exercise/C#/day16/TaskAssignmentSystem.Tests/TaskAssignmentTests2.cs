using FluentAssertions;
using Xunit;

namespace TaskAssignmentSystem.Tests
{
    public class TaskAssignmentTests2
    {
        private readonly List<Elf> _elves;
        private readonly TaskAssignment _system;

        public TaskAssignmentTests2()
        {
            // Arrange: Setup default list of elves for shared tests
            _elves = new List<Elf>
            {
                new(1, 5),  // Elf 1: Skill 5
                new(2, 10), // Elf 2: Skill 10
                new(3, 20)  // Elf 3: Skill 20
            };
            _system = new TaskAssignment(_elves);
        }

        // 1. Tests for ReportTaskCompletion
        [Fact]
        public void ReportTaskCompletion_ShouldIncrementTasks_WhenElfExists()
        {
            // Act
            var result = _system.ReportTaskCompletion(1);

            // Assert
            result.Should().BeTrue();
            _system.TotalTasksCompleted.Should().Be(1);
        }

        [Fact]
        public void ReportTaskCompletion_ShouldReturnFalse_WhenElfDoesNotExist()
        {
            // Act
            var result = _system.ReportTaskCompletion(99); // Nonexistent Elf ID

            // Assert
            result.Should().BeFalse();
            _system.TotalTasksCompleted.Should().Be(0);
        }

        // 2. Tests for ElfWithHighestSkill
        [Fact]
        public void ElfWithHighestSkill_ShouldReturnCorrectElf()
        {
            // Act
            var highestSkillElf = _system.ElfWithHighestSkill();

            // Assert
            highestSkillElf.Should().BeEquivalentTo(_elves.First(e => e.Id == 3));
        }

        // 3. Tests for AssignTask
        [Fact]
        public void AssignTask_ShouldAssignElf_WhenQualifiedElfExists()
        {
            // Act
            var assignedElf = _system.AssignTask(9); // Task requires skill 9

            // Assert
            assignedElf.Should().BeEquivalentTo(new Elf(2, 10)); // Elf 2 qualifies with skill 10
        }

        [Fact]
        public void AssignTask_ShouldReturnNull_WhenNoElfQualifies()
        {
            // Act
            var assignedElf = _system.AssignTask(50); // No elf has skill >= 50 + 1

            // Assert
            assignedElf.Should().BeNull();
        }

        // 4. Tests for IncreaseSkillLevel
        [Fact]
        public void IncreaseSkillLevel_ShouldIncreaseSkill_WhenElfExists()
        {
            // Act
            _system.IncreaseSkillLevel(1, 5); // Increase Elf 1's skill level by 5

            // Assert
            var updatedElf = _elves.First(e => e.Id == 1);
            updatedElf.SkillLevel.Should().Be(10); // Skill should now be 10
        }

        [Fact]
        public void IncreaseSkillLevel_ShouldDoNothing_WhenElfDoesNotExist()
        {
            // Act
            _system.IncreaseSkillLevel(99, 5); // Nonexistent Elf ID

            // Assert
            _elves.FirstOrDefault(e => e.Id == 1)?.SkillLevel.Should().Be(5); // Skills unchanged
        }

        // 5. Tests for DecreaseSkillLevel
        [Fact]
        public void DecreaseSkillLevel_ShouldReduceSkill_WhenElfExists()
        {
            // Act
            _system.DecreaseSkillLevel(1, 3); // Decrease skill of Elf 1 by 3

            // Assert
            var updatedElf = _elves.First(e => e.Id == 1);
            updatedElf.SkillLevel.Should().Be(2); // Skill should now be 2
        }

        [Fact]
        public void DecreaseSkillLevel_ShouldNotReduceSkillBelowOne()
        {
            // Act
            _system.DecreaseSkillLevel(1, 10); // Attempt to reduce below 1

            // Assert
            var updatedElf = _elves.First(e => e.Id == 1);
            updatedElf.SkillLevel.Should().Be(1); // Minimum skill enforced
        }

        [Fact]
        public void DecreaseSkillLevel_ShouldDoNothing_WhenElfDoesNotExist()
        {
            // Act
            _system.DecreaseSkillLevel(99, 5); // Nonexistent Elf ID

            // Assert
            _elves.FirstOrDefault(e => e.Id == 1)?.SkillLevel.Should().Be(5); // Skills unchanged
        }

        // 6. Tests for ReassignTask
        [Fact]
        public void ReassignTask_ShouldReturnTrue_WhenConditionsAreMet()
        {
            // Act
            var result = _system.ReassignTask(1, 2); // Elf 1 to Elf 2 (5 <= 10)

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ReassignTask_ShouldReturnFalse_WhenConditionsAreNotMet()
        {
            // Act
            var result = _system.ReassignTask(2, 1); // Elf 2 to Elf 1 (10 > 5)

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ReassignTask_ShouldReturnFalse_WhenOneElfDoesNotExist()
        {
            // Act
            var result = _system.ReassignTask(1, 99); // Nonexistent target elf

            // Assert
            result.Should().BeFalse();
        }

        // 7. Tests for ElvesBySkillDescending
        [Fact]
        public void ElvesBySkillDescending_ShouldReturnElvesInDescendingOrder()
        {
            // Act
            var sortedElves = _system.ElvesBySkillDescending();

            // Assert
            var expectedOrder = _elves.OrderByDescending(e => e.SkillLevel).ToList();
            sortedElves.Should().BeEquivalentTo(expectedOrder, options => options.WithStrictOrdering());
        }

        // 8. Tests for ResetAllSkillsToBaseline
        [Fact]
        public void ResetAllSkillsToBaseline_ShouldSetAllSkillsToGivenValue()
        {
            // Act
            _system.ResetAllSkillsToBaseline(10);

            // Assert
            _elves.All(e => e.SkillLevel == 10).Should().BeTrue();
        }
    }
}