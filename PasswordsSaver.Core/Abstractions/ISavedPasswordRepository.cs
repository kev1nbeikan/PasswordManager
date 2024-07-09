namespace PasswordsSaver.Core.Abstractions;

public interface ISavedPasswordRepository
{
    Task<bool> Save(SavedPassword savedPassword);
    Task<SavedPassword?> Get(Guid id);
    Task<List<SavedPassword>> GetAll();
}