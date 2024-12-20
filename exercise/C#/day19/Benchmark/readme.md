# Bench

## Performance result (Debug)

| Method               | NumberOfReindeers | Mean       | Error     | StdDev    | Median     | Allocated |
|--------------------- |------------------ |-----------:|----------:|----------:|-----------:|----------:|
| PowComputation       | 1                 |   3.553 ns | 0.0932 ns | 0.1336 ns |   3.515 ns |         - |
| RecursiveComputation | 1                 |   3.493 ns | 0.1054 ns | 0.1036 ns |   3.490 ns |         - |
| LoopComputation      | 1                 |   3.793 ns | 0.1143 ns | 0.1361 ns |   3.750 ns |         - |
| SwitchComputation    | 1                 |   5.242 ns | 0.1385 ns | 0.3575 ns |   5.097 ns |         - |
| PowComputation       | 10                |  27.008 ns | 0.1647 ns | 0.1376 ns |  27.019 ns |         - |
| RecursiveComputation | 10                |  51.837 ns | 0.6011 ns | 0.5328 ns |  51.661 ns |         - |
| LoopComputation      | 10                |  28.013 ns | 0.4947 ns | 0.3862 ns |  27.909 ns |         - |
| SwitchComputation    | 10                |   4.869 ns | 0.1375 ns | 0.1789 ns |   4.822 ns |         - |
| PowComputation       | 30                |  27.005 ns | 0.2625 ns | 0.2455 ns |  27.009 ns |         - |
| RecursiveComputation | 30                | 247.751 ns | 4.4063 ns | 3.6795 ns | 246.448 ns |         - |
| LoopComputation      | 30                |  87.952 ns | 0.9893 ns | 0.8261 ns |  87.999 ns |         - |
| SwitchComputation    | 30                |   5.278 ns | 0.1378 ns | 0.1289 ns |   5.224 ns |         - |
| PowComputation       | 50                |  26.709 ns | 0.2288 ns | 0.2140 ns |  26.728 ns |         - |
| RecursiveComputation | 50                | 457.705 ns | 7.6067 ns | 7.1154 ns | 457.611 ns |         - |
| LoopComputation      | 50                | 135.540 ns | 1.1071 ns | 0.8644 ns | 135.723 ns |         - |
| SwitchComputation    | 50                |   5.303 ns | 0.1429 ns | 0.1468 ns |   5.222 ns |         - |
