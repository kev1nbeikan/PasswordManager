using PasswordsSaver.Core;

namespace AngularApp1.Server.Contracts;

public class NewPasswordRequest
{
    public string Source { get; set; }
    public SourceType SourceType { get; set; }
    public string Password { get; set; }
}