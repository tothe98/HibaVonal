using Hibavonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public class OrderDto
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public int TotalAmount { get; set; }

    public EOrderStatus Status { get; set; }

    // Instead of exposing full OrderItem entity, use a DTO
    public List<OrderItem>? Items { get; set; }
}
