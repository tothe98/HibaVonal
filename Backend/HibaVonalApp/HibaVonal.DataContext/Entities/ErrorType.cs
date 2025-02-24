using System.ComponentModel.DataAnnotations;

namespace Hibavonal.DataContext.Entities;

public class ErrorType
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}

