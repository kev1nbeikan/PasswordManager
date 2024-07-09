using PasswordSaver.DataAccess.Entities;
using PasswordsSaver.Core;

namespace PasswordSaver.DataAccess.Extensions;

public static class ModelsExtensions
{
    public static SavedPassword ToCore(this SavedPasswordEntity savedPasswordEntity)
    {
        return new SavedPassword
        {
            Id = savedPasswordEntity.Id,
            Source = savedPasswordEntity.Source,
            SourceType = (SourceType)savedPasswordEntity.SourceType,
            Password = savedPasswordEntity.PasswordHash
        };
    }

    public static SavedPasswordEntity ToEntity(this SavedPassword savedPassword)
    {
        return new SavedPasswordEntity
        {
            Id = savedPassword.Id,
            Source = savedPassword.Source,
            SourceType = (int)savedPassword.SourceType,
            PasswordHash = savedPassword.Password
        };
    }
}