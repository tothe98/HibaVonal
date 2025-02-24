using System.ComponentModel.DataAnnotations;

namespace Hibavonal.DataContext.Entities;

public class SharedRoom : Room
{
    [Required]
    public string PersonInCharge { get; set; }

    [Required]
    public string PersonInChargeContact { get; set; }
}
