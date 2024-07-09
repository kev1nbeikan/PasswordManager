namespace PasswordSaver.DataAccess.Entities;

public class SavedPasswordEntity : BaseEntity
{
    
    public string Source { get; set; }
    public int SourceType { get; set; }
    public string PasswordHash { get; set; }
}