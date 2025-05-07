using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using HibaVonal.DataContext.Dtos;

namespace HibaVonal.Services.Services;

public interface IEquipmentService
{
    Task<List<EquipmentDto>> List();
    Task<EquipmentDto> Create(EquipmentCreateDto equipment);
    Task<EquipmentDto> Update(int id, EquipmentUpdateDto equipment);
    Task Delete(int id);
}
public class EquipmentService : IEquipmentService
{
    private readonly SQL _context;
    private readonly IMapper _mapper;
    public EquipmentService(SQL context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EquipmentDto>> List()
    {
        return await _context.Equipment.Include(e => e.ErrorType).Select(eq => _mapper.Map<EquipmentDto>(eq)).ToListAsync();
    }

    public async Task<EquipmentDto> Create(EquipmentCreateDto equipment)
    {
        if (_context.Equipment.Any(e => e.Name == equipment.Name))
        {
            throw new EquipmentAlreadyExistsException();
        }
        /*if (!_context.ErrorType.Any(e => e.Id == equipment.ErrorTypeId))
        {
            throw new ErrorTypeWithIdNotExistsException();
        }*/
        var result = await _context.Equipment.AddAsync(_mapper.Map<Equipment>(equipment));
        await _context.SaveChangesAsync();

        return _mapper.Map<EquipmentDto>(await _context.Equipment.Include(e => e.ErrorType).FirstOrDefaultAsync(e => e.Id == result.Entity.Id));
    }

    public async Task<EquipmentDto> Update(int id, EquipmentUpdateDto equipment)
    {
        if (!_context.Equipment.Any(e => e.Id == id))
        {
            throw new EquipmentWithIdNotExistsException();
        }
        if (equipment.ErrorTypeId>0 && !_context.ErrorType.Any(e => e.Id == equipment.ErrorTypeId))
        {
            throw new ErrorTypeWithIdNotExistsException();
        }
        EquipmentUpdateDto updateDto = new EquipmentUpdateDto() { Name = equipment.Name };
        if (equipment.ErrorTypeId > 0)
        {
            updateDto.ErrorTypeId = equipment.ErrorTypeId;
        }
        var eq = _mapper.Map<Equipment>(updateDto);
        eq.Id = id;
        _context.Equipment.Update(eq);
        await _context.SaveChangesAsync();

        return _mapper.Map<EquipmentDto>(await _context.Equipment.Include(e => e.ErrorType).FirstOrDefaultAsync(e => e.Id == id));
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
