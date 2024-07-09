using PasswordsSaver.Core;
using PasswordsSaver.Core.Abstractions;
using PasswordsSaver.Core.Abstractions.Infastructure;
using PasswordsSaver.Infastructure;

namespace PasswordSaver.Service;

public class PasswordSaverServiceWithInValidInput(ISavedPasswordRepository savedPasswordRepository)
    : IPasswordSaverService
{
    private readonly ISavedPasswordRepository _savedPasswordRepository = savedPasswordRepository;

    public async Task<IServiceResult<Guid>> Save(string source, SourceType sourceType, string password)
    {
        //TODO добавить хеширование или шифрование пароля

        var createdSavedPasswordResult = SavedPassword.CreateWithValidation(
            id: Guid.NewGuid(),
            source,
            sourceType,
            password,
            DateTime.Now
        );

        if (!createdSavedPasswordResult.IsSuccess)
            return ServiceResult<Guid>.Failure(createdSavedPasswordResult.ErrorMessage);

        await _savedPasswordRepository.Save(createdSavedPasswordResult.Data);
        return ServiceResult<Guid>.Success(createdSavedPasswordResult.Data.Id);
    }

    public async Task<IServiceResult<SavedPassword>> Get(Guid id)
    {
        var savedPassword = await _savedPasswordRepository.Get(id);
        return savedPassword is null
            ? ServiceResult<SavedPassword>.Failure("Пароля с таким айди не существует")
            : ServiceResult<SavedPassword>.Success(savedPassword!);
    }

    public async Task<IServiceResult<List<SavedPassword>>> GetAll()
    {
        var savedPasswords = await _savedPasswordRepository.GetAll();

        return ServiceResult<List<SavedPassword>>.Success(savedPasswords);
    }
}