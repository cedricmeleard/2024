namespace ToyProduction.Domain;

public class Toy(string name, State state)
{
    public string Name { get; } = name;
    public State State { get; set; } = state;
    public void ToProduction(IToyRepository repository)
    {
        if (this is not { State: State.Unassigned }) {
            return;
        }
        
        State = State.InProduction;
        repository.Save(this);
    }
}

public enum State
{
    Unassigned,
    InProduction,
    Completed
}