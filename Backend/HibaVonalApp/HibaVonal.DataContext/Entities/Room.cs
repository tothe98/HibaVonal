using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hibavonal.DataContext.Entities;

public abstract class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Floor { get; set; }

    public int DormitoryId { get; set; }

    [ForeignKey("DormitoryId")]
    public Dormitory? Dormitory { get; set; }
    public IList<Equipment> Equipments { get; set; }
}
