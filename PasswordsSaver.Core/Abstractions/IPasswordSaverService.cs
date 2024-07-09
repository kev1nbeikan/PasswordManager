using PasswordsSaver.Core.Abstractions.Infastructure;

namespace PasswordsSaver.Core.Abstractions;

public interface IPasswordSaverService
{
    Task<IServiceResult<Guid>> Save(
        string source,
        SourceType sourceType,
        string password
    );

    Task<IServiceResult<SavedPassword>> Get(Guid id);
    Task<IServiceResult<List<SavedPassword>>> GetAll();
}