using HibaVonal.DataContext.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hibavonal.DataContext.Entities;

public class PersonalRoom : Room
{
    [Required]
    public int Number { get; set; }

    public ICollection<User>? Residents { get; } = new List<User>();
}
