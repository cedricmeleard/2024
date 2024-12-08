using LanguageExt;
using LanguageExt.Common;

namespace ToyProduction.Domain;

public class Toy(string name, State state)
{
    private State _state = state;
    public string Name { get; } = name;
    public Either<Toy, Error> StartProduction()
    {
        if (IsNotUnassigned) {
            return Error.New($"Toy {Name} is not unassigned");
        }
        
        _state = State.InProduction;
        return this;
    }

    private bool IsNotUnassigned => this is not { _state: State.Unassigned };
    public bool IsInProduction => this is { _state: State.InProduction };
}

public enum State
{
    Unassigned,
    InProduction,
    Completed
}