using System.Collections;
using System.Net;
using ControlSystem.External;

namespace ControlSystem.Core;

public class ReindeerPowerUnits
{
    public readonly List<ReindeerPowerUnit> _reindeerPowerUnits;

    private ReindeerPowerUnits(List<ReindeerPowerUnit> reindeerPowerUnits)
    {
        _reindeerPowerUnits = reindeerPowerUnits;
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
}

public class System
{
    private const int XmasSpirit = 40;
    private readonly Dashboard _dashboard;
    
    public SleighEngineStatus Status { get; set; }
    public SleighAction Action { get; set; }
    
    private readonly ReindeerPowerUnits _reindeerPowerUnits;

    public System(MagicStable magicStable)
    {
        _dashboard = new Dashboard();
        _reindeerPowerUnits = ReindeerPowerUnits.CreateInstance(magicStable);
    }
    
    public void StartSystem()
    {
        _dashboard.DisplayStatus("Starting the sleigh...");
        Status = SleighEngineStatus.On;
        _dashboard.DisplayStatus("System ready.");
    }
    
    public void StopSystem()
    {
        _dashboard.DisplayStatus("Stopping the sleigh...");
        Status = SleighEngineStatus.Off;
        _dashboard.DisplayStatus("System shutdown.");
    }
    
    public void Ascend()
    {
        EnsureSleighIsStarted();
        if (!HasEnoughMagicPower()) throw new ReindeersNeedRestException();
        
        _dashboard.DisplayStatus("Ascending...");
        Action = SleighAction.Flying;
    }
    
    public void Descend()
    {
        EnsureSleighIsStarted();
        
        _dashboard.DisplayStatus("Descending...");
        Action = SleighAction.Hovering;
    }

    public void Park()
    {
        EnsureSleighIsStarted();
        
        _dashboard.DisplayStatus("Parking...");
        _reindeerPowerUnits._reindeerPowerUnits.ForEach(r => r.ResetHarnessing());
        
        Action = SleighAction.Parked;
    }

    private bool HasEnoughMagicPower()
        => _reindeerPowerUnits._reindeerPowerUnits.Sum(r => r.CheckMagicPower()) >= XmasSpirit;

    private void EnsureSleighIsStarted()
    {
        if (Status != SleighEngineStatus.On) throw new SleighNotStartedException();
    }
}