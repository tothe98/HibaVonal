using Hibavonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public class EquipmentDto
{

    public int Id { get; set; }

    public string Name { get; set; }

    public ErrorTypeDto? ErrorType { get; set; }
}

public class EquipmentCreateDto
{
    public string Name { get; set; }
    //public int ErrorTypeId { get; set; }
}

public class EquipmentUpdateDto
{
    public string Name { get; set; }

    public int? ErrorTypeId { get; set; }
}
