using System.ComponentModel.DataAnnotations;

namespace HibaVonal.DataContext.Dtos;

public class AddressDto
{
    [Required]
    public int ZIP { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public int HouseNumber { get; set; }
}
