namespace HibaVonal.DataContext.Dtos;

public class PersonalRoomDto : RoomDto
{
    public int Number { get; set; }

    public ICollection<UserDataDto>? Residents { get; set; } = new List<UserDataDto>();
}
