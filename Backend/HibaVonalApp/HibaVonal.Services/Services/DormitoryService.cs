using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IDormitoryService
{
    Task<IEnumerable<Dormitory>> List();
    Task Create(DormitoryDto dormitory);
    Task Update( int id ,DormitoryDto dormitory);
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

    public async Task Create(DormitoryDto dormitory)
    {
        ObjectValidatorService<DormitoryDto> v = new ObjectValidatorService<DormitoryDto>(dormitory);
        v.IsValid();
        Dormitory dor= new Dormitory();
        dor.Name = dormitory.Name;
        dor.Manager = dormitory.Manager;
        dor.ManagerContact = dormitory.ManagerContact;
        dor.NumberOfFloors = dormitory.NumberOfFloors;
        dor.PhoneNumber = dormitory.PhoneNumber;
        dor.AddressId = dormitory.AddressId;
        dor.Address= dormitory.Address;


        if (_context.Dormitory.Any(d => d.AddressId == dormitory.AddressId))
        {
            throw new DormitoryOnAddressAlreadyExistsException();
        }
        if (!_context.Address.Any(a => a.Id == dormitory.AddressId))
        {
            throw new AddressWithIdNotExistsException();
        }
        await _context.Dormitory.AddAsync(dor);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id, DormitoryDto dormitory)
    {
        ObjectValidatorService<DormitoryDto> v = new ObjectValidatorService<DormitoryDto>(dormitory);
        v.IsValid();
        Dormitory oldDormitory = _context.Dormitory.AsNoTracking().FirstOrDefault(d => d.Id == id);
        if (oldDormitory == null)
        {
            throw new DormitoryWithIdNotExistsException();
        }
        
        if (!_context.Address.Any(a => a.Id == dormitory.AddressId))
        {
            throw new AddressWithIdNotExistsException();
        }
        if (oldDormitory.AddressId != dormitory.AddressId && _context.Dormitory.Any(d => d.AddressId == dormitory.AddressId))
        {
            throw new DormitoryOnAddressAlreadyExistsException();
        }
        Dormitory updatedDormitory = _context.Dormitory.First(a => a.Id == id);
        updatedDormitory.Name = dormitory.Name;
        updatedDormitory.Manager = dormitory.Manager;
        updatedDormitory.ManagerContact = dormitory.ManagerContact;
        updatedDormitory.NumberOfFloors = dormitory.NumberOfFloors;
        updatedDormitory.PhoneNumber = dormitory.PhoneNumber;
        updatedDormitory.AddressId = dormitory.AddressId;
        updatedDormitory.Address = dormitory.Address;
        _context.Dormitory.Update(updatedDormitory);
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
