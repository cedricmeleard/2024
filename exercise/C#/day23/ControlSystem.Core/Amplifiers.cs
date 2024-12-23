namespace ControlSystem.Core;

public class Amplifiers
{
    public static Amplifiers Build()
    {
        var amplifiers = new Amplifiers();
        amplifiers._amplifiers.Push(new MagicPowerAmplifier(AmplifierType.Blessed));
        amplifiers._amplifiers.Push(new MagicPowerAmplifier(AmplifierType.Blessed));
        amplifiers._amplifiers.Push(new MagicPowerAmplifier(AmplifierType.Divine));
        return amplifiers;
    }

    private readonly Stack<MagicPowerAmplifier> _amplifiers = new();
    public MagicPowerAmplifier GetNext() => _amplifiers.Count != 0 
        ? _amplifiers.Pop() 
        : new MagicPowerAmplifier(AmplifierType.Basic);
}