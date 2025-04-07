namespace HibaVonal.DataContext.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    public EquipmentDto Equipment { get; set; }

    public int OrderId { get; set; }
}

public class OrderItemCreateDto
{
    public int Quantity { get; set; }

    public int Price { get; set; }

    public int EquipmentId { get; set; }
}
