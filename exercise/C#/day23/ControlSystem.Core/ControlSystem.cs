using ControlSystem.External;

namespace ControlSystem.Core;

public class System(MagicStable magicStable, Dashboard dashboard)
{
    public SleighEngineStatus GetStatus()
        => _status;

    public SleighAction Action { get; private set; }
    
    private readonly ReindeerPowerUnits _reindeerPowerUnits = ReindeerPowerUnits.CreateInstance(magicStable);
    private SleighEngineStatus _status = SleighEngineStatus.Off;

    public void StartSystem()
    {
        dashboard.DisplayStatus("Starting the sleigh...");
        _status = SleighEngineStatus.On;
        dashboard.DisplayStatus("System ready.");
    }
    
    public void StopSystem()
    {
        dashboard.DisplayStatus("Stopping the sleigh...");
        _status = SleighEngineStatus.Off;
        dashboard.DisplayStatus("System shutdown.");
    }
    
    public void Ascend()
    {
        EnsureSleighIsStarted();
        EnsureSufficientMagicPower();
        
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
        if (GetStatus() != SleighEngineStatus.On) throw new SleighNotStartedException();
    }
    
    private void EnsureSufficientMagicPower()
    {
        if (!_reindeerPowerUnits.HasEnoughMagicPower()) throw new ReindeersNeedRestException();
    }
}