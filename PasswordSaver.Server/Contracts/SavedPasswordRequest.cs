using PasswordsSaver.Core;

namespace AngularApp1.Server.Controllers;

public class SavedPasswordRequest
{
    public string Source { get; set; }
    public SourceType SourceType { get; set; }
    public string Password { get; set; }
}