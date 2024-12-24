using System.Collections;
using System.Net;
using ControlSystem.External;

namespace ControlSystem.Core;

public class System
{
    private const int XmasSpirit = 40;
    private readonly Dashboard _dashboard;
    private readonly MagicStable _magicStable;
    private readonly List<ReindeerPowerUnit> _reindeerPowerUnits;
    public SleighEngineStatus Status { get; set; }
    public SleighAction Action { get; set; }
    
    private readonly XmasTownAmplifiers _xmasTownAmplifiers = XmasTownAmplifiers.Build();
    
    public System(MagicStable magicStable)
    {
        _magicStable = magicStable;
        _dashboard = new Dashboard();
        _reindeerPowerUnits = BringAllReindeers();
    }

    private List<ReindeerPowerUnit> BringAllReindeers() =>
        _magicStable
            .GetAllReindeers()
            .OrderByDescending(r => r.GetMagicPower())
            .Select(reindeer => new ReindeerPowerUnit(reindeer,_xmasTownAmplifiers.GetNext()))
            .ToList();
    
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
        _reindeerPowerUnits.ForEach(r => r.ResetHarnessing());
        
        Action = SleighAction.Parked;
    }

    private bool HasEnoughMagicPower()
        => _reindeerPowerUnits.Sum(r => r.CheckMagicPower()) >= XmasSpirit;

    private void EnsureSleighIsStarted()
    {
        if (Status != SleighEngineStatus.On) throw new SleighNotStartedException();
    }
}