using NUnit.Framework;
using PasswordSaver.DataAccess.Repositories;
using PasswordSaver.Tests.Extensions;
using PasswordSaver.Tests.Fixtures;
using PasswordSaver.Tests.Utils;
using PasswordsSaver.Core;
using PasswordsSaver.Core.Abstractions;

namespace PasswordSaver.Tests;

public class PasswordSaverRepositoryTests
{
    private ISavedPasswordRepository _savedPasswordRepository;
    private SavedPassword _data;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        DbContextFixtures.OneTimeSetUp();
        _savedPasswordRepository =
            new SavedPasswordRepository(DbContextFixtures.context);
        _data = new SavedPassword
        {
            Id = Guid.NewGuid(),
            Source = "test",
            SourceType = SourceType.Email,
            Password = "test"
        };
    }


    [Test]
    public async Task Save()
    {
        
        
        var result = await _savedPasswordRepository.Save(_data);

        var savedPasswordFromRepo = await _savedPasswordRepository.Get(_data.Id);

        Assert.That(result, Is.True);
        Assert.That(savedPasswordFromRepo, Is.Not.Null);
        savedPasswordFromRepo!.AssertIsEqual(_data);
    }
}