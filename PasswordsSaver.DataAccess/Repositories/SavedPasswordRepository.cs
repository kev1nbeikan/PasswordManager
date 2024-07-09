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

    public async Task<List<SavedPassword>> GetAll()
    {
        return await _dbContext.SavedPasswords.Select(
                p => p.ToCore()
            )
            .ToListAsync();
    }
}