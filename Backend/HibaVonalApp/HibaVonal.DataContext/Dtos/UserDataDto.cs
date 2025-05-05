using HibaVonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public class UserDataDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public int? PersonalRoomId { get; set; }
    public IEnumerable<UserRoleDto> Roles { get; set; }
}

public class UserRoleDto
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }

}
