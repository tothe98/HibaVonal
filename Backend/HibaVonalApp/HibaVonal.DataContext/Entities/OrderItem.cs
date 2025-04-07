using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hibavonal.DataContext.Entities;

public class OrderItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public int EquipmentId { get; set; }
    [ForeignKey("EquipmentId")]
    public Equipment Equipment { get; set; }

    [Required]
    public int OrderId { get; set; }
    [ForeignKey("OrderId"), JsonIgnore]
    public Order Order { get; set; }
}
