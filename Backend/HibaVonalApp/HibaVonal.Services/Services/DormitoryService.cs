using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IDormitoryService
{
    Task<List<DormitoryDto>> List();
    Task<DormitoryDto> Create(DormitoryCreateDto dormitory);
    Task<DormitoryDto> Update(int id, DormitoryCreateDto dormitory);
    Task<bool> Delete(int id);
}

public class DormitoryService : IDormitoryService
{
    private readonly SQL _context;
    private readonly IMapper _mapper;

    public DormitoryService(SQL context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DormitoryDto>> List()
    {
        return await _context.Dormitory.Include(d => d.Address).Select(d => _mapper.Map<DormitoryDto>(d)).ToListAsync();
    }

    public async Task<DormitoryDto> Create(DormitoryCreateDto dormitory)
    {
        if (!await _context.Address.AnyAsync(a => a.Id == dormitory.AddressId))
        {
            throw new AddressWithIdNotExistsException();
        }
        if (await _context.Dormitory.AnyAsync(d => d.AddressId == dormitory.AddressId))
        {
            throw new DormitoryOnAddressAlreadyExistsException();
        }

        Dormitory dorm = _mapper.Map<Dormitory>(dormitory);
        await _context.Dormitory.AddAsync(dorm);
        await _context.SaveChangesAsync();

        return _mapper.Map<DormitoryDto>(dorm);
    }

    public async Task<DormitoryDto> Update(int id, DormitoryCreateDto dormitory)
    {
        Dormitory dorm = await _context.Dormitory.FindAsync(id);
        if (dorm == null)
        {
            throw new DormitoryWithIdNotExistsException();
        }
        if (!await _context.Address.AnyAsync(a => a.Id == dormitory.AddressId))
        {
            throw new AddressWithIdNotExistsException();
        }
        if (await _context.Dormitory.AnyAsync(d => d.AddressId == dormitory.AddressId && d.Id != id))
        {
            throw new DormitoryOnAddressAlreadyExistsException();
        }

        _mapper.Map(dormitory, dorm);
        _context.Dormitory.Update(dorm);
        await _context.SaveChangesAsync();

        return _mapper.Map<DormitoryDto>(dorm);
    }

    public async Task<bool> Delete(int id)
    {
        Dormitory dormitory = await _context.Dormitory.FindAsync(id);
        if (dormitory == null)
        {
            throw new DormitoryWithIdNotExistsException();
        }

        _context.Dormitory.Remove(dormitory);
        await _context.SaveChangesAsync();

        return true;
    }
}
