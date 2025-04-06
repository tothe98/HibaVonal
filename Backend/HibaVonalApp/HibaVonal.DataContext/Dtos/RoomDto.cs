using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public abstract class RoomDto
{
    public int Id { get; set; }

    public int Floor { get; set; }

    public string RoomType { get; set; } // To differentiate room types

    public IList<Equipment>? Equipments { get; set; }

    public Dormitory Dormitory { get; set; }
    public int DormitoryId { get; set; }
}

public class PersonalRoomDto : RoomDto
{
    public int Number { get; set; }
    public List<UserDataDto> Residents { get; set; }
}
public class SharedRoomDto : RoomDto
{
    public string PersonInCharge { get; set; }
    public string PersonInChargeContact { get; set; }
}
