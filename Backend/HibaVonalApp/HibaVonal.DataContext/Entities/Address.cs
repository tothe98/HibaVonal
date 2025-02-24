using System.ComponentModel.DataAnnotations;

namespace Hibavonal.DataContext.Entities;

public class Address
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ZIP { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public int HouseNumber { get; set; }
}
