using PasswordsSaver.Core;
using PasswordsSaver.Core.Abstractions;
using PasswordsSaver.Core.Abstractions.Infastructure;
using PasswordsSaver.Infastructure;

namespace PasswordSaver.Service;

public class PasswordSaverService(ISavedPasswordRepository savedPasswordRepository)
    : IPasswordSaverService
{
    private readonly ISavedPasswordRepository _savedPasswordRepository = savedPasswordRepository;

    public async Task<IServiceResult<SavedPassword>> Save(string source, SourceType sourceType, string password)
    {
        //TODO добавить хеширование или шифрование пароля
        var existedSavedPasswordResult = await _savedPasswordRepository.GetBySource(source);
        if (existedSavedPasswordResult is not null)
            return ServiceResult<SavedPassword>.Failure("Пароль для этого ресурса уже существует");

        var createdSavedPasswordResult = SavedPassword.CreateWithValidation(
            id: Guid.NewGuid(),
            source,
            sourceType,
            password,
            DateTime.Now
        );

        if (!createdSavedPasswordResult.IsSuccess)
            return ServiceResult<SavedPassword>.Failure(createdSavedPasswordResult.ErrorMessage);

        await _savedPasswordRepository.Save(createdSavedPasswordResult.Data);
        return ServiceResult<SavedPassword>.Success(createdSavedPasswordResult.Data);
    }

    public async Task<IServiceResult<SavedPassword>> Get(Guid id)
    {
        var savedPassword = await _savedPasswordRepository.Get(id);
        return savedPassword is null
            ? ServiceResult<SavedPassword>.Failure("Пароля с таким айди не существует")
            : ServiceResult<SavedPassword>.Success(savedPassword!);
    }

    public async Task<IServiceResult<IEnumerable<SavedPassword>>> GetAll()
    {
        var savedPasswords = await _savedPasswordRepository.GetAll();

        return ServiceResult<IEnumerable<SavedPassword>>.Success(savedPasswords);
    }

    public async Task<IServiceResult<SavedPassword>> Delete(Guid id)
    {
        var savedPassword = await _savedPasswordRepository.Get(id);
        if (savedPassword is null) return ServiceResult<SavedPassword>.Failure("Пароля с таким айди не существует");

        var deleteResult = await _savedPasswordRepository.Delete(id);
        return deleteResult is false
            ? ServiceResult<SavedPassword>.Failure("По ниезвестой пречине не удалось удалить пароль")
            : ServiceResult<SavedPassword>.Success(savedPassword);
    }

    public async Task<IServiceResult<IEnumerable<SavedPassword>>> Search(string search)
    {
        if (!IsValidSearch(search))
            return ServiceResult<IEnumerable<SavedPassword>>.Failure("Некорректные параметры поиска"); ;
        var savedPasswords = await _savedPasswordRepository.SearchBySource(FixSearch(search));
        return ServiceResult<IEnumerable<SavedPassword>>.Success(savedPasswords);
    }

    private bool IsValidSearch(string searchParams)
    {
        if (string.IsNullOrWhiteSpace(searchParams)) return false;
        return true;
    }

    private string FixSearch(string search)
    {
        return search.Trim().ToLower();
    }
}