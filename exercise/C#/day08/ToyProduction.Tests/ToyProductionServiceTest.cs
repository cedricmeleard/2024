using ToyProduction.Domain;
using ToyProduction.Services;
using ToyProduction.Tests.Doubles;

namespace ToyProduction.Tests;

public class ToyProductionServiceTest
{
    private const string ToyName = "Train";
    private readonly FakeElflogger<ToyProductionService> _loggerMock;
    private readonly InMemoryToyRepository _repository;
    private readonly ToyProductionService _service;
    public ToyProductionServiceTest()
    {
        _repository = new InMemoryToyRepository();
        _loggerMock = new FakeElflogger<ToyProductionService>();
        _service = new ToyProductionService(_repository, _loggerMock);
    }

    [Fact]
    public void assign_toy_to_an_elf_should_pass_the_item_in_production()
    {
        // Arrange
        _repository.Save(new Toy(ToyName, State.Unassigned));
        // Act
        _service.AssignToyToElf(ToyName);
        // Assert
        _repository.FindByName(ToyName)!
            .IsInProduction
            .Should()
            .BeTrue();
    }

    [Theory]
    [InlineData(State.InProduction)]
    [InlineData(State.Completed)]
    public void assign_toy_to_an_elf_when_not_unassigned_should_log_an_info(State initialState)
    {
        // Arrange
        _repository.Save(new Toy(ToyName, initialState));
        // Act
        _service.AssignToyToElf(ToyName);
        // Assert
        _loggerMock.Message.Should().Be($"Toy {ToyName} is not unassigned");
    }
}