using System.Text.RegularExpressions;
using PasswordsSaver.Core.Abstractions.Infastructure;
using PasswordsSaver.Infastructure;

namespace PasswordsSaver.Core;

public class SavedPassword
{
    public Guid Id { get; set; }
    public string Source { get; set; }
    public SourceType SourceType { get; set; }
    public string Password { get; set; }

    public DateTime CreatedDate { get; set; }


    public static IServiceResult<SavedPassword> CreateWithValidation(Guid id, string source, SourceType sourceType,
        string passwordHash, DateTime createdDate)
    {
        var result = new SavedPassword
        {
            Id = id,
            Source = source,
            SourceType = sourceType,
            Password = passwordHash,
            CreatedDate = createdDate
        };

        var error = result.IsValid();

        return error == null
            ? ServiceResult<SavedPassword>.Success(result)
            : ServiceResult<SavedPassword>.Failure(error);
    }

    private string? IsValid()
    {
        if (Id == Guid.Empty)
            return "Айди не может быть пустым";

        if (string.IsNullOrEmpty(Source))
            return "Источник не может быть пустым";

        if (SourceType == SourceType.Email && !IsValidEmail(Source))
            return "Некорректный адрес электронной почты";

        if (string.IsNullOrEmpty(Password))
        {
            return "Пароль не может быть пустым";
        }


        return null;
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
}