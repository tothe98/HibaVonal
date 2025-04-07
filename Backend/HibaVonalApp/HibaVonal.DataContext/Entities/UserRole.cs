using System.ComponentModel.DataAnnotations.Schema;

namespace HibaVonal.DataContext.Entities;

public class UserRole
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    //public User User { get; set; } = null!; -- körkörös referenciák miatt kikommentelve

    public int RoleId { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; } = null!;
}
