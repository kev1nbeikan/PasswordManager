using PasswordsSaver.Core;
using PasswordsSaver.Core.Abstractions.Infastructure;

namespace PasswordsSaver.Infastructure;

// TODO добавить хеширование или шифрование пароля
public class MyPasswordHasher : IPasswordHasher
{
    public string Generate(string password) =>
        password;

    public bool Verify(string password, string hash) =>
        true;

    public string Decrypt(string hash) =>
        hash;
}