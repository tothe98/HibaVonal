using System.ComponentModel.DataAnnotations;

namespace HibaVonal.DataContext.Entities;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public IList<User> Users { get; set; }
}
