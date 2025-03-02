using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace HibaVonal.Services.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> List();
        Task Add(Order order);
        Task Update(Order order);
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

        public async Task Add(Order order)
        {
            ObjectValidatorService<Order> v = new ObjectValidatorService<Order>(order);
            v.IsValid();
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Order order)
        {
            ObjectValidatorService<Order> v = new ObjectValidatorService<Order>(order);
            v.IsValid();
            Order oldOrder = _context.Order.AsNoTracking().FirstOrDefault(o => o.Id == order.Id);
            if (oldOrder==null)
            {
                throw new OrderWithIdNotExistsException();
            }
            //Not updating the order date if an update happens
            order.Date = oldOrder.Date;
            _context.Order.Update(order);
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
}
