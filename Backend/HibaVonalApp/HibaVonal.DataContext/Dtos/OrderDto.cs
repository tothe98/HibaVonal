namespace HibaVonal.DataContext.Dtos;

public class OrderDTO
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public int TotalAmount { get; set; }

    public EOrderStatus Status { get; set; }

    // Instead of exposing full OrderItem entity, use a DTO
    public List<OrderItemDto>? Items { get; set; }
}
