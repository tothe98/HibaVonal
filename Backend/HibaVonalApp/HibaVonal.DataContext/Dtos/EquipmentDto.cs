using Hibavonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public class EquipmentDto
{

    public int Id { get; set; }

    public string Name { get; set; }

    public ErrorTypeDto? ErrorType { get; set; }
}
