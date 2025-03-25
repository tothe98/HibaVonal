using Hibavonal.DataContext.Entities;
using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;
using HibaVonal.DataContext.Dtos;

namespace HibaVonal.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;
    public EquipmentController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    [HttpGet]
    public async Task<IEnumerable<Equipment>> List()
    {
        return await _equipmentService.List();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EquipmentDto equipment)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _equipmentService.Create(equipment);
            response.StatusCode = 200;
            response.Message = "Equipment added successfully";
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
        catch (EquipmentAlreadyExistsException ex)
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
    public async Task<IActionResult> Update(int id,EquipmentDto equipment)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _equipmentService.Update(id,equipment);
            response.StatusCode = 200;
            response.Message = "Equipment updated successfully";
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
        catch (EquipmentWithIdNotExistsException ex)
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
            await _equipmentService.Delete(id);
            response.StatusCode = 200;
            response.Message = "Equipment deleted successfully";
            return Ok(response);
        }
        catch (EquipmentWithIdNotExistsException ex)
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
