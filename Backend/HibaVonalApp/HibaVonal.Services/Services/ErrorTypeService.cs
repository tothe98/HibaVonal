using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IErrorTypeService
{
    Task<IEnumerable<ErrorType>> List();
    Task Create(ErrorTypeDto errorType);
    Task Update(int id ,ErrorTypeDto errorType);
    Task Delete(int id);
}
public class ErrorTypeService : IErrorTypeService
{
    private readonly SQL _context;
    public ErrorTypeService(SQL context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ErrorType>> List()
    {
        return await _context.ErrorType.ToListAsync();
    }

    public async Task Create(ErrorTypeDto errorType)
    {
        ObjectValidatorService<ErrorTypeDto> v = new ObjectValidatorService<ErrorTypeDto>(errorType);
        v.IsValid();
        ErrorType newErrorType = new ErrorType();
        newErrorType.Name = errorType.Name;
        if (!_context.ErrorType.Any(e => e.Name == errorType.Name))
        {
            await _context.ErrorType.AddAsync(newErrorType);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ErrorTypeAlreadyExistsException();
        }
    }

    public async Task Update(int id , ErrorTypeDto errorType)
    {
        ObjectValidatorService<ErrorTypeDto> v = new ObjectValidatorService<ErrorTypeDto>(errorType);
        v.IsValid();
        if (!_context.ErrorType.Any(e => e.Id == id))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        ErrorType newErrorType = _context.ErrorType.FirstOrDefault(e => e.Id == id);
        newErrorType.Name = errorType.Name;
        _context.ErrorType.Update(newErrorType);
        await _context.SaveChangesAsync();
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
