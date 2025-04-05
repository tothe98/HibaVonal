using HibaVonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public class PersonalRoomDto : RoomDto
{
    public int Number { get; set; }

    public ICollection<User>? Residents { get; set; } = new List<User>();
}
