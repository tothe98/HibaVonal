using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;
using HibaVonal.DataContext.Dtos;

namespace HibaVonal.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AddressDto>>> List()
    {
        try
        {
            List<AddressDto> result = await _addressService.List();
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
    public async Task<ActionResult<APIResponse>> Create([FromBody] AddressCreateDto address)
    {
        try
        {
            AddressDto result = await _addressService.Create(address);
            return Ok(new APIResponse
            {
                Data = result,
                StatusCode = 200,
                Message = "Address added successfully"
            });
        }
        catch (AddressAlreadyExistsException ex)
        {
            return Conflict(new APIResponse
            {
                StatusCode = 409,
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
    public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] AddressCreateDto address)
    {
        try
        {
            AddressDto result = await _addressService.Update(id, address);
            return Ok(new APIResponse
            {
                Data = result,
                StatusCode = 200,
                Message = "Address updated successfully"
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
        catch (AddressAlreadyExistsException ex)
        {
            return Conflict(new APIResponse
            {
                StatusCode = 409,
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
            await _addressService.Delete(id);
            return Ok(new APIResponse
            {
                StatusCode = 200,
                Message = "Address deleted successfully"
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
        catch (Exception)
        {
            return StatusCode(500, new APIResponse
            {
                StatusCode = 500,
                Message = "An unexpected error occurred."
            });
        }
    }
}
