using System.ComponentModel.DataAnnotations;

namespace Hibavonal.DataContext.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    public DateTime? Date { get; set; } = DateTime.Now;

    [Required]
    public int TotalAmount { get; set; }

    [Required]
    public EOrderStatus Status { get; set; }

    public IList<OrderItem>? Items { get; set; } = new List<OrderItem>();
}
