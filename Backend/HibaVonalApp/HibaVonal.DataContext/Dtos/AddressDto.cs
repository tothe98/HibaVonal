namespace HibaVonal.DataContext.Dtos;

public class AddressDto
{
    public int Id { get; set; }

    public int ZIP { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public int HouseNumber { get; set; }
}

public class AddressCreateDto
{
    public int ZIP { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public int HouseNumber { get; set; }
}
