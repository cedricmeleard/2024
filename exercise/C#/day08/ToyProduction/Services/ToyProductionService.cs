using ToyProduction.Domain;

namespace ToyProduction.Services;

public class ToyProductionService(IToyRepository repository)
{
    public void AssignToyToElf(string toyName) 
        => repository
            .FindByName(toyName)?
            .ToProduction(repository);
}