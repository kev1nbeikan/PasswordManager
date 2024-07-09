using NUnit.Framework;
using PasswordSaver.Tests.Utils;

namespace PasswordSaver.Tests.Fixtures;

[SetUpFixture]
public static class DbContextFixtures
{
    private const string ConnectionString = "Host=localhost;Database=savedPasswords-test;Username=postgres;Password=1";


    public static FakeDbContext context;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        Console.WriteLine($"Используется строка подключения {ConnectionString}");
        context = FakeDbContext.Create(ConnectionString);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    [OneTimeTearDown]
    public static async void BaseTearDown()
    {
        await context.DisposeAsync();
    }
}