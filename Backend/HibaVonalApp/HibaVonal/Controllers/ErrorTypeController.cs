using Hibavonal.DataContext.Entities;
using HibaVonal.Services.Exceptions;
using HibaVonal.Services.Services;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class ErrorTypeController : ControllerBase
{
    private readonly IErrorTypeService _errorTypeService;
    public ErrorTypeController(IErrorTypeService errorTypeService)
    {
        _errorTypeService = errorTypeService;
    }

    [HttpGet]
    public async Task<IEnumerable<ErrorType>> List()
    {
        return await _errorTypeService.List();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ErrorType errorType)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _errorTypeService.Create(errorType);
            response.StatusCode = 200;
            response.Message = "ErrorType added successfully";
            return Ok(response);
        }
        catch (ErrorTypeWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (MandatoryPropertyEmptyException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (ErrorTypeAlreadyExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 202;
            response.Message = ex.InnerException?.Message;
        }
        return BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ErrorType errorType)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _errorTypeService.Update(errorType);
            response.StatusCode = 200;
            response.Message = "ErrorType updated successfully";
            return Ok(response);
        }
        catch (MandatoryPropertyEmptyException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (ErrorTypeWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 202;
            response.Message = ex.InnerException?.Message;
        }
        return BadRequest(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _errorTypeService.Delete(id);
            response.StatusCode = 200;
            response.Message = "ErrorType deleted successfully";
            return Ok(response);
        }
        catch (ErrorTypeWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 202;
            response.Message = ex.InnerException?.Message;
        }
        return BadRequest(response);
    }
}
