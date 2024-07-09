using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PasswordSaver.DataAccess;

namespace PasswordSaver.Tests.Utils;

public class FakeDbContext(DbContextOptions<PasswordsSaverDbContext> options) : PasswordsSaverDbContext(options)
{
    public static FakeDbContext Create(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<PasswordsSaverDbContext>()
            .UseInMemoryDatabase("FakeDbContextTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new FakeDbContext(contextOptions);
    }
}