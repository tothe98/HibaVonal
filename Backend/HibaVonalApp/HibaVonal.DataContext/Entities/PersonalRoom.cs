using System.ComponentModel.DataAnnotations;

namespace Hibavonal.DataContext.Entities;

public class PersonalRoom : Room
{
    [Required]
    public int Nubmer { get; set; }

    [Required]
    public IList<User> Residents { get; set; }
}
