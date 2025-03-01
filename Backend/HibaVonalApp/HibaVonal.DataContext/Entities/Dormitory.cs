using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hibavonal.DataContext.Entities;

public class Dormitory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Manager { get; set; }

    [Required]
    public string ManagerContact { get; set; }

    [Required]
    public int NumberOfFloors { get; set; }

    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public int AddressId { get; set; }
    [ForeignKey("AddressId")]
    public Address? Address { get; set; }
}
