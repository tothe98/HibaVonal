using Hibavonal.DataContext.Entities;
using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;
using HibaVonal.DataContext.Dtos;

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
    public async Task<IEnumerable<Dormitory>> List()
    {
        return await _dormitoryService.List();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DormitoryDto dormitory)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _dormitoryService.Create(dormitory);
            response.StatusCode = 200;
            response.Message = "Dormitory added successfully";
            return Ok(response);
        }
        catch (AddressWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (MandatoryPropertyEmptyException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (DormitoryOnAddressAlreadyExistsException ex)
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
    public async Task<IActionResult> Update(int id , [FromBody]DormitoryDto dormitory)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _dormitoryService.Update(id, dormitory);
            response.StatusCode = 200;
            response.Message = "Dormitory updated successfully";
            return Ok(response);
        }
        catch (AddressWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (MandatoryPropertyEmptyException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (DormitoryWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (DormitoryOnAddressAlreadyExistsException ex)
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
            await _dormitoryService.Delete(id);
            response.StatusCode = 200;
            response.Message = "Dormitory deleted successfully";
            return Ok(response);
        }
        catch (DormitoryWithIdNotExistsException ex)
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
