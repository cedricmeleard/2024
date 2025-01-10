using BenchmarkDotNet.Attributes;
using static Travel.SantaTravelCalculator;

namespace Benchmark;

[MemoryDiagnoser]
public class Benchmark
{
    [Params(1, 10, 30, 50)] public int NumberOfReindeers;

    [Benchmark]
    public ulong PowComputation()
    {
        return CalculateDistanceForReindeersMathPow(NumberOfReindeers);
    }
    
    [Benchmark]
    public ulong RecursiveComputation()
    {
        return CalculateTotalDistanceRecursively(NumberOfReindeers);
    }
    
    [Benchmark]
    public ulong LoopComputation()
    {
        return CalculateTotalDistanceLoop(NumberOfReindeers);
    }
    
    [Benchmark]
    public ulong SwitchComputation()
    {
        return SantaTravelValueBasedCalculator.CalculateTotalDistance(NumberOfReindeers);
    }
}