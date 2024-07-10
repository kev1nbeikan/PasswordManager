using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PasswordSaver.DataAccess.Entities;
using PasswordSaver.DataAccess.Extensions;
using PasswordsSaver.Core;
using PasswordsSaver.Core.Abstractions;

namespace PasswordSaver.DataAccess.Repositories;

public class SavedPasswordRepository(PasswordsSaverDbContext dbContext) : ISavedPasswordRepository
{
    private readonly PasswordsSaverDbContext _dbContext = dbContext;

    public async Task<bool> Save(SavedPassword savedPassword)
    {
        await _dbContext.SavedPasswords.AddAsync(savedPassword.ToEntity());
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<SavedPassword?> Get(Guid id)
    {
        var savedPasswordEntity = await _dbContext.SavedPasswords.FirstOrDefaultAsync(p => p.Id == id);
        return savedPasswordEntity?.ToCore();
    }

    public Task<SavedPassword?> GetBySource(string source)
    {
        return _dbContext.SavedPasswords
            .Where(p => p.Source == source)
            .Select(p => p.ToCore())
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SavedPassword>> GetAll()
    {
        return await _dbContext.SavedPasswords.OrderByDescending(p => p.CreatedDate).Select(
                p => p.ToCore()
            )
            .ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _dbContext.SavedPasswords
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<IEnumerable<SavedPassword>> SearchBySource(string searchWorld)
    {
        return await _dbContext.SavedPasswords.Where(p => p.Source.ToLower().Contains(searchWorld.ToLower()))
            .OrderByDescending(p => p.CreatedDate)
            .Select(p => p.ToCore())
            .ToListAsync();
    }
}