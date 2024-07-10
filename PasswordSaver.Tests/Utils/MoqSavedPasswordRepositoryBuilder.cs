using Moq;
using PasswordsSaver.Core;
using PasswordsSaver.Core.Abstractions;

namespace PasswordSaver.Tests.Utils;

public class MoqSavedPasswordRepositoryBuilder
{
    private Mock<ISavedPasswordRepository> moqSavedPasswordRepository = new Mock<ISavedPasswordRepository>();

    public static MoqSavedPasswordRepositoryBuilder Create()
    {
        return new MoqSavedPasswordRepositoryBuilder();
    }

    public ISavedPasswordRepository Build()
    {
        return moqSavedPasswordRepository.Object;
    }

    public MoqSavedPasswordRepositoryBuilder SetupBase()
    {
        ;
        moqSavedPasswordRepository.Setup(x => x.Save(It.IsAny<SavedPassword>()))
            .Returns(Task.FromResult(true));
        moqSavedPasswordRepository.Setup(x => x.Get(It.IsAny<Guid>()))
            .Returns(Task.FromResult(new SavedPassword()));
        moqSavedPasswordRepository.Setup(x => x.GetAll())
            .Returns(Task.FromResult<IEnumerable<SavedPassword>>(new List<SavedPassword>()));

        return this;
    }

    public MoqSavedPasswordRepositoryBuilder ReturnNullOnGet()
    {
        moqSavedPasswordRepository.Setup(x => x.Get(It.IsAny<Guid>()))
            .Returns(Task.FromResult<SavedPassword>(null));
        return this;
    }
}