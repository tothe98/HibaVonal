using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IOrderService
{
    Task<List<OrderDto>> List();
    Task<OrderDto> Create(OrderCreateDto order);
    Task<OrderDto> Update(int id, OrderCreateDto order);
    Task Delete(int id);
}

public class OrderService : IOrderService
{
    private readonly SQL _context;
    private readonly IMapper _mapper;
    public OrderService(SQL context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderDto>> List()
    {
        return await _context.Order.Include(d => d.Items).ThenInclude(i => i.Equipment).Select(o => _mapper.Map<OrderDto>(o)).ToListAsync();
    }

    public async Task<OrderDto> Create(OrderCreateDto orderCreateDto)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var order = _mapper.Map<Order>(orderCreateDto);
                var newOrder = await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();

                foreach (var item in orderCreateDto.Items)
                {
                    if (!_context.Equipment.Any(e => e.Id == item.EquipmentId))
                    {
                        throw new EquipmentWithIdNotExistsException();
                    }
                    if (item.Quantity <= 0)
                    {
                        throw new MandatoryPropertyEmptyException("Quantity must be greater than 0");
                    }
                    var orderitem = _mapper.Map<OrderItem>(item);
                    orderitem.OrderId = newOrder.Entity.Id;
                    await _context.OrderItem.AddAsync(orderitem);
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();

                return _mapper.Map<OrderDto>(await _context.Order.Include(o => o.Items).ThenInclude(i => i.Equipment).Where(or => or.Id == newOrder.Entity.Id).FirstOrDefaultAsync());
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<OrderDto> Update(int id, OrderCreateDto orderUpdateDto)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                Order oldOrder = _context.Order.AsNoTracking().FirstOrDefault(o => o.Id == id);
                if (oldOrder == null)
                {
                    throw new OrderWithIdNotExistsException();
                }

                var order = _mapper.Map<Order>(orderUpdateDto);
                order.Date = oldOrder.Date;
                order.Id = id;
                var newOrder = _context.Order.Update(order);
                await _context.SaveChangesAsync();

                var orderItems = _context.OrderItem.Where(i => i.OrderId == id);

                foreach (var dtoOrderItem in orderUpdateDto.Items)
                {
                    var dbOrderItem = orderItems.FirstOrDefault(i => i.EquipmentId == dtoOrderItem.EquipmentId);
                    if (dbOrderItem != default)
                    {
                        dbOrderItem.Quantity = dtoOrderItem.Quantity;
                        dbOrderItem.Price = dtoOrderItem.Price;
                        dbOrderItem.EquipmentId = dtoOrderItem.EquipmentId;
                        _context.OrderItem.Update(dbOrderItem);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        if (!_context.Equipment.Any(e => e.Id == dtoOrderItem.EquipmentId))
                        {
                            throw new EquipmentWithIdNotExistsException();
                        }
                        if (dtoOrderItem.Quantity <= 0)
                        {
                            throw new MandatoryPropertyEmptyException("Quantity must be greater than 0");
                        }
                        var orderitem = _mapper.Map<OrderItem>(dtoOrderItem);
                        orderitem.OrderId = newOrder.Entity.Id;
                        await _context.OrderItem.AddAsync(orderitem);
                        await _context.SaveChangesAsync();
                    }
                }

                await transaction.CommitAsync();
                return _mapper.Map<OrderDto>(await _context.Order.Include(o => o.Items).ThenInclude(i => i.Equipment).Where(or => or.Id == newOrder.Entity.Id).FirstOrDefaultAsync());
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }





    }

    public async Task Delete(int id)
    {
        var order = _context.Order.FirstOrDefault(d => d.Id == id);
        if (order == null)
        {
            throw new OrderWithIdNotExistsException();
        }

        var orderItems = _context.OrderItem.Where(i => i.OrderId == id);
        if (orderItems != null)
        {
            _context.OrderItem.RemoveRange(orderItems);
        }

        _context.Order.Remove(order);
        await _context.SaveChangesAsync();
    }
}
