using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public class RoomDto
{
    public int Id { get; set; }

    public int Floor { get; set; }

    public string RoomType { get; set; } // To differentiate room types

    public IList<Equipment>? Equipments { get; set; }

    public Dormitory Dormitory { get; set; }
    public int DormitoryId { get; set; }
}


/*
public abstract class AbstractRoomDto
{
    public int Id { get; set; }

    public int Floor { get; set; }

    public string RoomType { get; set; }

    public List<EquipmentDto>? Equipments { get; set; }
}

public class PersonalRoomDto2 : AbstractRoomDto
{
    public string RoomType { get; set; } = "PersonalRoom";
    public int Number { get; set; }
}

public class SharedRoomDto2 : AbstractRoomDto
{
    public string RoomType { get; set; } = "SharedRoom";
    public string PersonInCharge { get; set; }
    public string PersonInChargeContact { get; set; }
}
*/