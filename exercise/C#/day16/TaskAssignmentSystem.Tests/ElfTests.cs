using FluentAssertions;
using Xunit;

namespace TaskAssignmentSystem.Tests;

public class ElfTests
{
    [Fact]
    public void Elf_ShouldIncreaseSkill()
    {
        var elf = new Elf(1, 10);
        elf.IncreaseSkill(5);

        elf.SkillLevel.Should().Be(15);
    }

    [Fact]
    public void Elf_ShouldNotDecreaseSkillBelowOne()
    {
        var elf = new Elf(1, 5);
        elf.DecreaseSkill(10);

        elf.SkillLevel.Should().Be(1);
    }
    
    [Fact]
    public void Elf_ShouldThrowException_WhenIdIsInvalid()
    {
        Action act = () => new Elf(0, 10); // Invalid ID, must be positive

        act.Should().Throw<ArgumentException>().WithMessage("Id must be positive.");
    }

    [Fact]
    public void Elf_ShouldThrowException_WhenSkillLevelIsInvalid()
    {
        Action act = () => new Elf(1, 0); // Invalid Skill Level, must be positive

        act.Should().Throw<ArgumentException>().WithMessage("Skill level must be positive.");
    }
    
    [Fact]
    public void Elf_ShouldThrowException_WhenIncrementIsInvalid()
    {
        var elf = new Elf(1, 10);

        Action act = () => elf.IncreaseSkill(0); // Invalid increment, must be positive

        act.Should().Throw<ArgumentException>().WithMessage("Increment must be positive.");
    }

    [Fact]
    public void Elf_ShouldThrowException_WhenDecrementIsInvalid()
    {
        var elf = new Elf(1, 10);

        Action act = () => elf.DecreaseSkill(0); // Invalid decrement, must be positive

        act.Should().Throw<ArgumentException>().WithMessage("Decrement must be positive.");
    }

    [Fact]
    public void Elf_ShouldDecreaseSkill_ToMinimumOfOne()
    {
        var elf = new Elf(1, 3);

        elf.DecreaseSkill(10); // Exceeds current skill level

        elf.SkillLevel.Should().Be(1); // Skill cannot go below 1
    }
}