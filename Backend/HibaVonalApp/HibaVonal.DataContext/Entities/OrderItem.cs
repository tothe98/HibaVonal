using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hibavonal.DataContext.Entities;

public class OrderItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int Price { get; set; }

    public int EquipmentId { get; set; }

    [ForeignKey("EquipmentId")]
    public Equipment? Equipment { get; set; }
}
