using ControlSystem.External;

namespace ControlSystem.Core;

public class System(MagicStable magicStable, Dashboard dashboard)
{
    public SleighEngineStatus Status { get; set; }
    public SleighAction Action { get; set; }
    
    private readonly ReindeerPowerUnits _reindeerPowerUnits = ReindeerPowerUnits.CreateInstance(magicStable);

    public void StartSystem()
    {
        dashboard.DisplayStatus("Starting the sleigh...");
        Status = SleighEngineStatus.On;
        dashboard.DisplayStatus("System ready.");
    }
    
    public void StopSystem()
    {
        dashboard.DisplayStatus("Stopping the sleigh...");
        Status = SleighEngineStatus.Off;
        dashboard.DisplayStatus("System shutdown.");
    }
    
    public void Ascend()
    {
        EnsureSleighIsStarted();
        if (!_reindeerPowerUnits.HasEnoughMagicPower()) throw new ReindeersNeedRestException();
        
        dashboard.DisplayStatus("Ascending...");
        Action = SleighAction.Flying;
    }
    
    public void Descend()
    {
        EnsureSleighIsStarted();
        
        dashboard.DisplayStatus("Descending...");
        
        Action = SleighAction.Hovering;
    }

    public void Park()
    {
        EnsureSleighIsStarted();
        
        dashboard.DisplayStatus("Parking...");
        
        _reindeerPowerUnits.ResetHarnessing();
        
        Action = SleighAction.Parked;
    }

    private void EnsureSleighIsStarted()
    {
        if (Status != SleighEngineStatus.On) throw new SleighNotStartedException();
    }
}