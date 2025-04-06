using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItem>> List();
   /* Task Create(OrderItemDto orderItem);
    Task Update(int id, OrderItemDto orderItem);
    Task Delete(int id);*/
}

public class OrderItemService : IOrderItemService
{
    private readonly SQL _context;
    public OrderItemService(SQL context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> List()
    {
        return await _context.OrderItem.Include(o => o.Equipment).Include(o => o.Order).ToListAsync();
    }

   /* public async Task Create(OrderItemCreateDto orderItem)
    {
        ObjectValidatorService<OrderItemDto> v = new ObjectValidatorService<OrderItemDto>(orderItem);
        v.IsValid();
        if (!_context.Equipment.Any(e => e.Id == orderItem.EquipmentId))
        {
            throw new EquipmentWithIdNotExistsException();
        }
        if (!_context.Order.Any(o => o.Id == orderItem.OrderId))
        {
            throw new OrderWithIdNotExistsException();
        }
        OrderItem newOrderItem = new OrderItem();
        newOrderItem.Quantity = orderItem.Quantity;
        newOrderItem.Price = orderItem.Price;
        newOrderItem.EquipmentId = orderItem.EquipmentId;
        newOrderItem.Equipment = orderItem.Equipment;
        newOrderItem.OrderId = orderItem.OrderId;
        newOrderItem.Order= orderItem.Order;



        await _context.OrderItem.AddAsync(newOrderItem);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id , OrderItemDto orderItem)
    {
        ObjectValidatorService<OrderItemDto> v = new ObjectValidatorService<OrderItemDto>(orderItem);
        v.IsValid();
        if (!_context.OrderItem.Any(o => o.Id == id))
        {
            throw new OrderItemWithIdNotExistsException();
        }
        if (!_context.Equipment.Any(e => e.Id == orderItem.EquipmentId))
        {
            throw new EquipmentWithIdNotExistsException();
        }
        if (!_context.Order.Any(o => o.Id == orderItem.OrderId))
        {
            throw new OrderWithIdNotExistsException();
        }
        OrderItem newOrderItem = _context.OrderItem.First(o => o.Id == id);
        newOrderItem.Quantity = orderItem.Quantity;
        newOrderItem.Price = orderItem.Price;
        newOrderItem.EquipmentId = orderItem.EquipmentId;
        newOrderItem.Equipment = orderItem.Equipment;
        newOrderItem.OrderId = orderItem.OrderId;
        newOrderItem.Order = orderItem.Order;
        _context.OrderItem.Update(newOrderItem);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var orderItem = _context.OrderItem.FirstOrDefault(d => d.Id == id);
        if (orderItem == null)
        {
            throw new OrderItemWithIdNotExistsException();
        }
        _context.OrderItem.Remove(orderItem);
        await _context.SaveChangesAsync();
    }*/
}
