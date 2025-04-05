namespace HibaVonal.DataContext.Dtos;

public class DormitoryDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Manager { get; set; }

    public string ManagerContact { get; set; }

    public int NumberOfFloors { get; set; }

    public string PhoneNumber { get; set; }

    public AddressDto Address { get; set; }
}

public class DormitoryCreateDto
{
    public string Name { get; set; }

    public string Manager { get; set; }

    public string ManagerContact { get; set; }

    public int NumberOfFloors { get; set; }

    public string PhoneNumber { get; set; }

    public int AddressId { get; set; }
}
