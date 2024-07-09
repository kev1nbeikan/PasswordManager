namespace PasswordsSaver.Core.Abstractions;

public interface ISavedPasswordRepository
{
    Task<bool> Save(SavedPassword savedPassword);
    Task<SavedPassword?> Get(Guid id);
    Task<List<SavedPassword>> GetAll();
    Task<bool> Delete(Guid id);
    Task<IEnumerable<SavedPassword>> SearchBySource(string searchWorld);
}