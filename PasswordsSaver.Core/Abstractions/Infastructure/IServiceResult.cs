namespace PasswordsSaver.Core.Abstractions.Infastructure;

public interface IServiceResult<T>
{
    bool IsSuccess { get; }
    T Data { get; }
    string ErrorMessage { get; }
}