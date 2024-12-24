using ControlSystem.External;

namespace ControlSystem.Core;

public class ReindeerPowerUnit
{
    private readonly Reindeer _reindeer;
    private readonly MagicPowerAmplifier _amplifier;

    public ReindeerPowerUnit(Reindeer reindeer, MagicPowerAmplifier magicPowerAmplifier)
    {
        _reindeer = reindeer;
        _amplifier = magicPowerAmplifier;
    }
    
    public float HarnessMagicPower()
    {
        if (_reindeer.NeedsRest()) return 0;
        
        _reindeer.TimesHarnessing++;
        return _amplifier.Amplify(_reindeer.GetMagicPower());
    }

    public float CheckMagicPower()
    {
        return _reindeer.GetMagicPower();
    }

    public void ResetHarnessing() => _reindeer.TimesHarnessing = 0;
}