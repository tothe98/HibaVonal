using Hibavonal.DataContext.Entities;
using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;
using HibaVonal.DataContext.Dtos;

namespace HibaVonal.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomDto>>> List()
    {
        return Ok(await _roomService.List());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomDto room)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _roomService.Create(room);
            response.StatusCode = 200;
            response.Message = "Room added successfully";
            return Ok(response);
        }
        catch (NotSupportedException)
        {
            response.StatusCode = 202;
            response.Message = "The given RoomType does not exist";
        }
        catch (RoomWithNumberExistsException ex)
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
        catch (Exception ex)
        {
            response.StatusCode = 202;
            response.Message = ex.InnerException?.Message;
        }
        return BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id ,RoomDto room)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _roomService.Update(id ,room);
            response.StatusCode = 200;
            response.Message = "Room updated successfully";
            return Ok(response);
        }
        catch (RoomRoomTypeNotMatchException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (RoomWithNumberExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (DormitoryWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (MandatoryPropertyEmptyException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (RoomWithIdNotExistsException ex)
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
            await _roomService.Delete(id);
            response.StatusCode = 200;
            response.Message = "Room deleted successfully";
            return Ok(response);
        }
        catch (RoomWithIdNotExistsException ex)
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
