using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace HibaVonal.Services.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> List();
        Task Add(OrderItem orderItem);
        Task Update(OrderItem orderItem);
        Task Delete(int id);
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
            return await _context.OrderItem.Include(o => o.Equipment).Include(o=>o.Order).ToListAsync();
        }

        public async Task Add(OrderItem orderItem)
        {
            ObjectValidatorService<OrderItem> v = new ObjectValidatorService<OrderItem>(orderItem);
            v.IsValid();
            if (!_context.Equipment.Any(e => e.Id == orderItem.EquipmentId))
            {
                throw new EquipmentWithIdNotExistsException();
            }
            if (!_context.Order.Any(o => o.Id == orderItem.OrderId))
            {
                throw new OrderWithIdNotExistsException();
            }
            await _context.OrderItem.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }
        public async Task Update(OrderItem orderItem)
        {
            ObjectValidatorService<OrderItem> v = new ObjectValidatorService<OrderItem>(orderItem);
            v.IsValid();
            if (!_context.OrderItem.Any(o => o.Id == orderItem.Id))
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
            _context.OrderItem.Update(orderItem);
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
        }
    }   
}
