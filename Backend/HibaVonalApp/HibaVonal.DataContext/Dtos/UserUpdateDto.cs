namespace HibaVonal.DataContext.Dtos;

public class UserUpdateDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public int? PersonalRoomId { get; set; }
}
