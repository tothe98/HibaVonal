using System.ComponentModel.DataAnnotations;

namespace Hibavonal.DataContext.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int TotalAmount { get; set; }

    [Required]
    public OrderStatus Status { get; set; }

    [Required]
    public IList<OrderItem> Items { get; set; }
}

