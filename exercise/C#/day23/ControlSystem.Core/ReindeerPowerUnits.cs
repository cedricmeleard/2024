using ControlSystem.External;

namespace ControlSystem.Core;

public class ReindeerPowerUnits
{
    private const int XmasSpirit = 40;
    private readonly List<ReindeerPowerUnit> _powerUnits;
    private ReindeerPowerUnits(List<ReindeerPowerUnit> reindeerPowerUnits)
    {
        _powerUnits = reindeerPowerUnits;
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

    public void ResetHarnessing() => _powerUnits.ForEach(r => r.ResetHarnessing());
    public bool HasEnoughMagicPower() => _powerUnits.Sum(r => r.HarnessMagicPower()) >= XmasSpirit;
}