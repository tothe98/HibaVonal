using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace HibaVonal.Services.Services
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> List();
        Task Add(Equipment equipment);
        Task Update(Equipment equipment);
        Task Delete(int id);
    }
    public class EquipmentService : IEquipmentService
    {
        private readonly SQL _context;
        public EquipmentService(SQL context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Equipment>> List()
        {
            return await _context.Equipment.Include(e => e.ErrorType).ToListAsync();
        }

        public async Task Add(Equipment equipment)
        {
            ObjectValidatorService<Equipment> v = new ObjectValidatorService<Equipment>(equipment);
            v.IsValid();
            if (_context.Equipment.Any(e => e.Name == equipment.Name))
            {
                throw new EquipmentAlreadyExistsException();
            }
            if (!_context.ErrorType.Any(e => e.Id == equipment.ErrorTypeId))
            {
                throw new ErrorTypeWithIdNotExistsException();
            }
            await _context.Equipment.AddAsync(equipment);
            await _context.SaveChangesAsync();            
        }
        public async Task Update(Equipment equipment)
        {
            ObjectValidatorService<Equipment> v = new ObjectValidatorService<Equipment>(equipment);
            v.IsValid();
            if (!_context.Equipment.Any(e => e.Id == equipment.Id))
            {
                throw new EquipmentWithIdNotExistsException();
            }
            if (!_context.ErrorType.Any(e => e.Id == equipment.ErrorTypeId))
            {
                throw new ErrorTypeWithIdNotExistsException();
            }
            _context.Equipment.Update(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var equipment = _context.Equipment.FirstOrDefault(d => d.Id == id);
            if (equipment == null)
            {
                throw new EquipmentWithIdNotExistsException();
            }
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
        }
    }   
}
