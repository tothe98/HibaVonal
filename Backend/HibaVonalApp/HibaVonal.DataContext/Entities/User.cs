using Hibavonal.DataContext.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HibaVonal.DataContext.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }

    public int RoleId { get; set; }

    [Required, ForeignKey("RoleId")]
    public Role Role { get; set; }

    // These should depend on whether it has "Resident" role or not
    /*public int DormitoryId { get; set; }

    [ForeignKey("DormitoryId")]
    public Dormitory Dormitory { get; set; }*/

    public int RoomId { get; set; }

    [ForeignKey("RoomId")]
    public Room Room { get; set; }
}
