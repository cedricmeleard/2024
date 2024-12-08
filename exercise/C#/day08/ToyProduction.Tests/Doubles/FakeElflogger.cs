using ToyProduction.Services;

namespace ToyProduction.Tests.Doubles;

public class FakeElflogger<T> : IElflogger<T>
{
    public string? Message { get; private set; }

    public void Info(string? message)
    {
        Message = message;
    }
}