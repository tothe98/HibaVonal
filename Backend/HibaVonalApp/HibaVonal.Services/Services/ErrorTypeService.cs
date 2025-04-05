using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IErrorTypeService
{
    Task<List<ErrorTypeDto>> List();
    Task<ErrorTypeDto> Create(ErrorTypeCreateUpdateDto errorType);
    Task<ErrorTypeDto> Update(int id, ErrorTypeCreateUpdateDto errorType);
    Task Delete(int id);
}
public class ErrorTypeService : IErrorTypeService
{
    private readonly SQL _context;
    private readonly IMapper _mapper;
    public ErrorTypeService(SQL context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ErrorTypeDto>> List()
    {
        return await _context.ErrorType.Select(e => _mapper.Map<ErrorTypeDto>(e)).ToListAsync();
    }

    public async Task<ErrorTypeDto> Create(ErrorTypeCreateUpdateDto errorType)
    {
        if (!_context.ErrorType.Any(e => e.Name == errorType.Name))
        {
            await _context.ErrorType.AddAsync(_mapper.Map<ErrorType>(errorType));
            await _context.SaveChangesAsync();
            return _mapper.Map<ErrorTypeDto>(await _context.ErrorType.FirstOrDefaultAsync(e => e.Name == errorType.Name));
        }
        else
        {
            throw new ErrorTypeAlreadyExistsException();
        }
    }

    public async Task<ErrorTypeDto> Update(int id, ErrorTypeCreateUpdateDto errorType)
    {
        if (!_context.ErrorType.Any(e => e.Id == id))
        {
            throw new ErrorTypeWithIdNotExistsException();
        }
        var errType = _mapper.Map<ErrorType>(errorType);
        errType.Id = id;
        _context.ErrorType.Update(errType);
        await _context.SaveChangesAsync();
        return _mapper.Map<ErrorTypeDto>(await _context.ErrorType.FirstOrDefaultAsync(e => e.Id == id));
    }

    public async Task Delete(int id)
    {
        var errorType = _context.ErrorType.FirstOrDefault(e => e.Id == id);
        if (errorType == null)
        {
            throw new ErrorTypeWithIdNotExistsException();
        }
        _context.ErrorType.Remove(errorType);
        await _context.SaveChangesAsync();
    }
}
