using Ardalis.GuardClauses;
using Communication.Domain;

namespace Communication.Tests;

public class ReindeerTester
{
    private readonly string _name;
    private string _location;
    private int _numberOfDaysForComingBack;
    private ReindeerTester(string name)
    {
        _name = name;
    }
    public static ReindeerTester Named(string name)
    {
        return new ReindeerTester(name);
    }
    public ReindeerTester LocatedAt(string location, int numberOfDaysForComingBack = 0)
    {
        _location = location;
        _numberOfDaysForComingBack = numberOfDaysForComingBack;
        return this;
    }

    public Reindeer Summon()
    {
        Guard.Against.NullOrEmpty(_name);
        Guard.Against.NullOrEmpty(_location);
        Guard.Against.Negative(_numberOfDaysForComingBack);

        return new Reindeer(
            new ReindeerName(_name),
            new Location(_location, _numberOfDaysForComingBack)
        );
    }
}