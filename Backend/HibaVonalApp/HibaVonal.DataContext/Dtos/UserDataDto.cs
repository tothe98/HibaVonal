using HibaVonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public class UserDataDto
{
    public string Email { get; set; }

    public string Name { get; set; }
    public IEnumerable<UserRole> Roles { get; set; }
}
