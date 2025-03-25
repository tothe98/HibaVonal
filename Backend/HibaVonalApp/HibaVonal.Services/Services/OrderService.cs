using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> List();
    Task Create(OrderDto order);
    Task Update(int id , OrderDto order);
    Task Delete(int id);
}

public class OrderService : IOrderService
{
    private readonly SQL _context;
    public OrderService(SQL context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> List()
    {
        return await _context.Order.Include(d => d.Items).ToListAsync();
    }

    public async Task Create(OrderDto order)
    {
        ObjectValidatorService<OrderDto> v = new ObjectValidatorService<OrderDto>(order);
        v.IsValid();
        Order newOrder = new Order();
        newOrder.Date = order.Date;
        newOrder.TotalAmount = order.TotalAmount;
        newOrder.Status= order.Status;
        newOrder.Items = order.Items;
        await _context.Order.AddAsync(newOrder);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id, OrderDto order)
    {
        ObjectValidatorService<OrderDto> v = new ObjectValidatorService<OrderDto>(order);
        v.IsValid();
        Order oldOrder = _context.Order.AsNoTracking().FirstOrDefault(o => o.Id == id);
        if (oldOrder == null)
        {
            throw new OrderWithIdNotExistsException();
        }
        Order newOrder = _context.Order.First(o => o.Id == id);
        //Not updating the order date if an update happens
        newOrder.Date = oldOrder.Date;
        newOrder.TotalAmount = order.TotalAmount;
        newOrder.Status = order.Status;
        newOrder.Items = order.Items;
        _context.Order.Update(newOrder);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var order = _context.Order.FirstOrDefault(d => d.Id == id);
        if (order == null)
        {
            throw new OrderWithIdNotExistsException();
        }
        _context.Order.Remove(order);
        await _context.SaveChangesAsync();
    }
}
