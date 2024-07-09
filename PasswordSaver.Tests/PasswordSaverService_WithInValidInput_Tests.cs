using NUnit.Framework;
using PasswordSaver.Service;
using PasswordSaver.Tests.Utils;
using PasswordsSaver.Core;
using PasswordsSaver.Core.Abstractions;

namespace PasswordSaver.Tests;

public class PasswordSaverService_WithInValidInput_Tests
{
    private readonly IPasswordSaverService _passwordSaverService;
    private readonly ISavedPasswordRepository _savedPasswordRepository;


    public PasswordSaverService_WithInValidInput_Tests()
    {
        _savedPasswordRepository = MoqSavedPasswordRepositoryBuilder.Create().SetupBase().ReturnNullOnGet().Build();
        _passwordSaverService = new PasswordSaverServiceWithInValidInput(_savedPasswordRepository);
    }

    [Test]
    public async Task Save()
    {
        var result = await _passwordSaverService.Save("test", SourceType.Email, "test");

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.Not.Null);
        });
    }

    [Test]
    public async Task Get()
    {
        var result = await _passwordSaverService.Get(Guid.NewGuid());

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.Not.Null);
        });
    }
    
}