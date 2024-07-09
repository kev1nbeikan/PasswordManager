namespace AngularApp1.Server.Contracts;

public record SavedPasswordResponse
{
    public Guid Id { get; set; }
    public string Source { get; set; }
    public int SourceType { get; set; }
    public string Password { get; set; }
    public string CreatedDate { get; set; }
}