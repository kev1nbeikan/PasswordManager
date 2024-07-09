using System.ComponentModel.DataAnnotations;

namespace PasswordSaver.DataAccess.Entities;

public class BaseEntity
{
    [Required]
    public Guid Id { get; set; }
}