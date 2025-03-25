using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using HibaVonal.DataContext.Dtos;

namespace HibaVonal.Services.Services;

public interface IEquipmentService
{
    Task<IEnumerable<Equipment>> List();
    Task Create(EquipmentDto equipment);
    Task Update(int id ,EquipmentDto equipment);
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

    public async Task Create(EquipmentDto equipment)
    {
        ObjectValidatorService<EquipmentDto> v = new ObjectValidatorService<EquipmentDto>(equipment);
        v.IsValid();
        if (_context.Equipment.Any(e => e.Name == equipment.Name))
        {
            throw new EquipmentAlreadyExistsException();
        }
        if (!_context.ErrorType.Any(e => e.Id == equipment.ErrorTypeId))
        {
            throw new ErrorTypeWithIdNotExistsException();
        }
        Equipment newEquipment = new Equipment();
        newEquipment.ErrorType = equipment.ErrorType;
        newEquipment.ErrorTypeId = equipment.ErrorTypeId;
        newEquipment.Name = equipment.Name;

        await _context.Equipment.AddAsync(newEquipment);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id ,EquipmentDto equipment)
    {
        ObjectValidatorService<EquipmentDto> v = new ObjectValidatorService<EquipmentDto>(equipment);
        v.IsValid();
        if (!_context.Equipment.Any(e => e.Id == id))
        {
            throw new EquipmentWithIdNotExistsException();
        }
        if (!_context.ErrorType.Any(e => e.Id == equipment.ErrorTypeId))
        {
            throw new ErrorTypeWithIdNotExistsException();
        }
        Equipment newEquipment = _context.Equipment.FirstOrDefault(e => e.Id == id);
        newEquipment.ErrorType = equipment.ErrorType;
        newEquipment.ErrorTypeId = equipment.ErrorTypeId;
        newEquipment.Name = equipment.Name;

        _context.Equipment.Update(newEquipment);
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
