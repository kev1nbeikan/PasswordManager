namespace PasswordsSaver.Core.Abstractions.Infastructure;

public interface IPasswordHasher
{
    string Generate(string password);

    bool Verify(string password, string hash);
    public string Decrypt(string hash);
}