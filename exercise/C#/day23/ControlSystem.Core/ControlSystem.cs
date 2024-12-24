using ControlSystem.External;

namespace ControlSystem.Core;

public class System
{
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
        if (!_reindeerPowerUnits.HasEnoughMagicPower(this)) throw new ReindeersNeedRestException();
        
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
        
        _reindeerPowerUnits.ResetHarnessing();
        
        Action = SleighAction.Parked;
    }

    private void EnsureSleighIsStarted()
    {
        if (Status != SleighEngineStatus.On) throw new SleighNotStartedException();
    }
}