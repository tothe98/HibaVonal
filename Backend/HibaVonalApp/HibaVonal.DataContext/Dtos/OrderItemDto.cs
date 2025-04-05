using Hibavonal.DataContext.Entities;

namespace HibaVonal.DataContext.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    public Equipment Equipment { get; set; }

    public int EquipmentId { get; set; }

    public Order Order { get; set; }
    public int OrderId { get; set; }
}
