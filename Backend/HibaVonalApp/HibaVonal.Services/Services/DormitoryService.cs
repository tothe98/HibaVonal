using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace HibaVonal.Services.Services
{
    public interface IDormitoryService
    {
        Task<IEnumerable<Dormitory>> List();
        Task Add(Dormitory dormitory);
        Task Update(Dormitory dormitory);
        Task Delete(int id);
    }
    public class DormitoryService : IDormitoryService
    {
        private readonly SQL _context;
        public DormitoryService(SQL context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Dormitory>> List()
        {
            return await _context.Dormitory.Include(d => d.Address).ToListAsync();
        }

        public async Task Add(Dormitory dormitory)
        {
            ObjectValidatorService<Dormitory> v = new ObjectValidatorService<Dormitory>(dormitory);
            v.IsValid();
            if (!_context.Dormitory.Any(d => d.AddressId == dormitory.AddressId))
            {
                await _context.Dormitory.AddAsync(dormitory);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new DormitoryAlreadyExistsException();
            }
        }
        public async Task Update(Dormitory dormitory)
        {
            ObjectValidatorService<Dormitory> v = new ObjectValidatorService<Dormitory>(dormitory);
            v.IsValid();
            if (!_context.Dormitory.Any(d => d.AddressId == dormitory.AddressId))
            {
                throw new DormitoryWithIdNotExistsException();
            }
            _context.Dormitory.Update(dormitory);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var dormitory = _context.Dormitory.FirstOrDefault(d => d.Id == id);
            if (dormitory == null)
            {
                throw new DormitoryWithIdNotExistsException();
            }
            _context.Dormitory.Remove(dormitory);
            await _context.SaveChangesAsync();
        }
    }   
}
