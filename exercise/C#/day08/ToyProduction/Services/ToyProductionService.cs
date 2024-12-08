using ToyProduction.Domain;

namespace ToyProduction.Services;

public class ToyProductionService(IToyRepository repository)
{
    // this method is a bit of miss leading because it does not assign the toy to the elf
    // but rather change the status to production
    public void AssignToyToElf(string toyName) 
        => repository
            .FindByName(toyName)?
            .ToProduction(repository);
}