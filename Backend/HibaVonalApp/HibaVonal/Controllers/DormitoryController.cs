using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DormitoryController : ControllerBase
{
    private readonly IDormitoryService _dormitoryService;

    public DormitoryController(IDormitoryService dormitoryService)
    {
        _dormitoryService = dormitoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DormitoryDto>>> List()
    {
        try
        {
            List<DormitoryDto> result = await _dormitoryService.List();
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, new APIResponse
            {
                StatusCode = 500,
                Message = "An unexpected error occurred."
            });
        }
    }

    [HttpPost]
    public async Task<ActionResult<APIResponse>> Create([FromBody] DormitoryCreateDto dormitory)
    {
        try
        {
            DormitoryDto result = await _dormitoryService.Create(dormitory);
            return Ok(new APIResponse
            {
                Data = result,
                StatusCode = 200,
                Message = "Dormitory added successfully"
            });
        }
        catch (AddressWithIdNotExistsException ex)
        {
            return NotFound(new APIResponse
            {
                StatusCode = 404,
                Message = ex.Message
            });
        }
        catch (MandatoryPropertyEmptyException ex)
        {
            return BadRequest(new APIResponse
            {
                StatusCode = 400,
                Message = ex.Message
            });
        }
        catch (DormitoryOnAddressAlreadyExistsException ex)
        {
            return Conflict(new APIResponse
            {
                StatusCode = 409,
                Message = ex.Message
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new APIResponse
            {
                StatusCode = 500,
                Message = "An unexpected error occurred"
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] DormitoryCreateDto dormitory)
    {
        try
        {
            await _dormitoryService.Update(id, dormitory);
            return Ok(new APIResponse
            {
                StatusCode = 200,
                Message = "Dormitory updated successfully"
            });
        }
        catch (AddressWithIdNotExistsException ex)
        {
            return NotFound(new APIResponse
            {
                StatusCode = 404,
                Message = ex.Message
            });
        }
        catch (MandatoryPropertyEmptyException ex)
        {
            return BadRequest(new APIResponse
            {
                StatusCode = 400,
                Message = ex.Message
            });
        }
        catch (DormitoryWithIdNotExistsException ex)
        {
            return NotFound(new APIResponse
            {
                StatusCode = 404,
                Message = ex.Message
            });
        }
        catch (DormitoryOnAddressAlreadyExistsException ex)
        {
            return Conflict(new APIResponse
            {
                StatusCode = 409,
                Message = ex.Message
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new APIResponse
            {
                StatusCode = 500,
                Message = "An unexpected error occurred."
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<APIResponse>> Delete(int id)
    {
        try
        {
            await _dormitoryService.Delete(id);
            return Ok(new APIResponse
            {
                StatusCode = 200,
                Message = "Dormitory deleted successfully"
            });
        }
        catch (DormitoryWithIdNotExistsException ex)
        {
            return NotFound(new APIResponse
            {
                StatusCode = 404,
                Message = ex.Message
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new APIResponse
            {
                StatusCode = 500,
                Message = "An unexpected error occurred"
            });
        }
    }
}
