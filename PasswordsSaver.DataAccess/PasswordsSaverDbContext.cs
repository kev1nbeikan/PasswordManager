using Microsoft.EntityFrameworkCore;
using PasswordSaver.DataAccess.Entities;

namespace PasswordSaver.DataAccess;

public class PasswordsSaverDbContext(DbContextOptions<PasswordsSaverDbContext> options) : DbContext(options)
{
    public DbSet<SavedPasswordEntity> SavedPasswords { get; set; }
}