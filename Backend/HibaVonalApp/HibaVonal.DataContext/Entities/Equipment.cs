using HibaVonal.DataContext.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hibavonal.DataContext.Entities;

public class Equipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int? ErrorTypeId { get; set; } = null;
    [ForeignKey("ErrorTypeId")]
    public ErrorType? ErrorType { get; set; }

    public IList<RoomEquipment> RoomEquipments { get; set; } = new List<RoomEquipment>();
}
