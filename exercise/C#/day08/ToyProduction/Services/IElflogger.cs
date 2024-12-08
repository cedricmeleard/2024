namespace ToyProduction.Services;

public interface IElflogger<T>
{
    void Info(string? message);
}