using PasswordsSaver.Core.Abstractions.Infastructure;

namespace PasswordsSaver.Core.Abstractions;

public interface IPasswordSaverService
{
    Task<IServiceResult<SavedPassword>> Save(string source,
        SourceType sourceType,
        string password);

    Task<IServiceResult<SavedPassword>> Get(Guid id);
    Task<IServiceResult<List<SavedPassword>>> GetAll();

    Task<IServiceResult<SavedPassword>> Delete(Guid id);
}