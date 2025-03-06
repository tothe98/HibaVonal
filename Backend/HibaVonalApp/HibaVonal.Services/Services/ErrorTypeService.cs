using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IErrorTypeService
{
    Task<IEnumerable<ErrorType>> List();
    Task Create(ErrorType errorType);
    Task Update(ErrorType errorType);
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

    public async Task Create(ErrorType errorType)
    {
        ObjectValidatorService<ErrorType> v = new ObjectValidatorService<ErrorType>(errorType);
        v.IsValid();
        if (!_context.ErrorType.Any(e => e.Name == errorType.Name))
        {
            await _context.ErrorType.AddAsync(errorType);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ErrorTypeAlreadyExistsException();
        }
    }

    public async Task Update(ErrorType errorType)
    {
        ObjectValidatorService<ErrorType> v = new ObjectValidatorService<ErrorType>(errorType);
        v.IsValid();
        if (!_context.ErrorType.Any(e => e.Id == errorType.Id))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        _context.ErrorType.Update(errorType);
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
