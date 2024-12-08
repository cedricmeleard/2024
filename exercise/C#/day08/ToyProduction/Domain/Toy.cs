namespace ToyProduction.Domain;

public class Toy(string name, State state)
{
    private State _state = state;
    public string Name { get; } = name;
    public void StartProduction(IToyRepository repository)
    {
        if (IsNotUnassigned) {
            return;
        }
        
        _state = State.InProduction;
        repository.Save(this);
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