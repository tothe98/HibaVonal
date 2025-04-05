using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IAddressService
{
    Task<List<AddressDto>> List();
    Task<AddressDto> Create(AddressCreateDto address);
    Task<AddressDto> Update(int id, AddressCreateDto address);
    Task<bool> Delete(int id);
}

public class AddressService : IAddressService
{
    private readonly SQL _context;
    private readonly IMapper _mapper;

    public AddressService(SQL context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AddressDto>> List()
    {
        return await _context.Address.Select(a => _mapper.Map<AddressDto>(a)).ToListAsync();
    }

    public async Task<AddressDto> Create(AddressCreateDto address)
    {
        if (await _context.Address.AnyAsync(a => a.ZIP == address.ZIP && a.HouseNumber == address.HouseNumber))
        {
            throw new AddressAlreadyExistsException();
        }

        Address addr = _mapper.Map<Address>(address);
        await _context.Address.AddAsync(addr);
        await _context.SaveChangesAsync();

        return _mapper.Map<AddressDto>(addr);
    }

    public async Task<AddressDto> Update(int id, AddressCreateDto address)
    {
        Address addr = await _context.Address.FindAsync(id);
        if (addr == null)
        {
            throw new AddressWithIdNotExistsException();
        }
        if (await _context.Address.AnyAsync(a => a.Id != id && a.ZIP == address.ZIP && a.HouseNumber == address.HouseNumber))
        {
            throw new AddressAlreadyExistsException();
        }

        _mapper.Map(address, addr);
        _context.Address.Update(addr);
        await _context.SaveChangesAsync();

        return _mapper.Map<AddressDto>(addr);
    }

    public async Task<bool> Delete(int id)
    {
        Address addr = await _context.Address.FindAsync(id);
        if (addr == null)
        {
            throw new AddressWithIdNotExistsException();
        }

        _context.Address.Remove(addr);
        await _context.SaveChangesAsync();

        return true;
    }
}
