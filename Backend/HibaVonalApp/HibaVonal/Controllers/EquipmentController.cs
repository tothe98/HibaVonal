using Hibavonal.DataContext.Entities;
using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;
using HibaVonal.DataContext.Dtos;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize("MaintenanceWorker")]
    public async Task<ActionResult<List<EquipmentDto>>> List()
    {
        try
        {
            return Ok(await _equipmentService.List());

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
    [Authorize("Admin")]
    public async Task<ActionResult<APIResponse>> Create([FromBody] EquipmentCreateDto equipment)
    {
        APIResponse response = new APIResponse();
        try
        {
            var result = await _equipmentService.Create(equipment);
            response.StatusCode = 200;
            response.Data = result;
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

    [HttpPut("{id}")]
    [Authorize("MaintenanceWorker")]
    public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] EquipmentUpdateDto equipment)
    {
        APIResponse response = new APIResponse();
        try
        {
            var result = await _equipmentService.Update(id, equipment);
            response.Data = result;
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

    [HttpDelete("{id}")]
    [Authorize("Admin")]
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
