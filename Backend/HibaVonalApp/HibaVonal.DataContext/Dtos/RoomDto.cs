using HibaVonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public abstract class AbstractRoomDto
{
    public int Id { get; set; }

    public int Floor { get; set; }

    public string RoomType { get; set; }

    public List<EquipmentDto>? Equipments { get; set; }
}

public class PersonalRoomDto : AbstractRoomDto
{
    public string RoomType { get; set; } = "PersonalRoom";
    public int Number { get; set; }
}

public class SharedRoomDto : AbstractRoomDto
{
    public string RoomType { get; set; } = "SharedRoom";
    public string PersonInCharge { get; set; }
    public string PersonInChargeContact { get; set; }
}
