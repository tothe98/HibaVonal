namespace HibaVonal.DataContext.Dtos;

public class OrderDto
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public int TotalAmount { get; set; }

    public string Status { get; set; }
    
    public List<OrderItemDto>? Items { get; set; }
}

public class OrderCreateDto
{
    public DateTime? Date { get; set; } = DateTime.Now;

    public EOrderStatus? Status { get; set; } = EOrderStatus.Accepted;
    
    public List<OrderItemCreateDto> Items { get; set; }
}

public class OrderStatusUpdateDto
{
    public EOrderStatus Status { get; set; }
}
