using ControlSystem.External;

namespace ControlSystem.Core;

public class ReindeerPowerUnits
{
    private const int XmasSpirit = 40;
    public List<ReindeerPowerUnit> PowerUnits { get; }
    private ReindeerPowerUnits(List<ReindeerPowerUnit> reindeerPowerUnits)
    {
        PowerUnits = reindeerPowerUnits;
    }

    public static ReindeerPowerUnits CreateInstance(MagicStable magicStable)
    {
        var  xmasTownAmplifiers = XmasTownAmplifiers.Build();
        var reindeerPowerUnits = BringAllReindeers(magicStable, xmasTownAmplifiers);
        return new ReindeerPowerUnits(reindeerPowerUnits);
    }
    
    private static List<ReindeerPowerUnit> BringAllReindeers(MagicStable magicStable, XmasTownAmplifiers xmasTownAmplifiers) =>
        magicStable
            .GetAllReindeers()
            .OrderByDescending(r => r.GetMagicPower())
            .Select(reindeer => new ReindeerPowerUnit(reindeer,xmasTownAmplifiers.GetNext()))
            .ToList();

    public void ResetHarnessing() => PowerUnits.ForEach(r => r.ResetHarnessing());
    public bool HasEnoughMagicPower(System system) => PowerUnits.Sum(r => r.CheckMagicPower()) >= XmasSpirit;
}