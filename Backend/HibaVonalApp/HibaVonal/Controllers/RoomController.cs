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
[Authorize("Admin")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<RoomDto>>> List()
    {
        return Ok(await _roomService.List());
    }

    [HttpPost]
    public async Task<ActionResult<APIResponse>> CreatePersonalRoom([FromBody] PersonalRoomCreateDto room)
    {
        APIResponse response = new APIResponse();
        try
        {
            var result = await _roomService.CreatePersonalRoom(room);
            response.Data = result;
            response.StatusCode = 200;
            response.Message = "PersonalRoom added successfully";
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
    public async Task<ActionResult<APIResponse>> CreateSharedRoom([FromBody] SharedRoomCreateDto room)
    {
        APIResponse response = new APIResponse();
        try
        {
            var result = await _roomService.CreateSharedRoom(room);
            response.Data = result;
            response.StatusCode = 200;
            response.Message = "SharedRoom added successfully";
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

    [HttpPut("{id}")]
    public async Task<ActionResult<APIResponse>> UpdatePersonalRoom(int id ,PersonalRoomCreateDto room)
    {
        APIResponse response = new APIResponse();
        try
        {
            var result = await _roomService.UpdatePersonalRoom(id , room);
            response.Data = result;
            response.StatusCode = 200;
            response.Message = "PersonalRoom updated successfully";
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

    [HttpPut("{id}")]
    public async Task<ActionResult<APIResponse>> UpdateSharedRoom(int id, SharedRoomCreateDto room)
    {
        APIResponse response = new APIResponse();
        try
        {
            var result = await _roomService.UpdateSharedRoom(id, room);
            response.Data = result;
            response.StatusCode = 200;
            response.Message = "SharedRoom updated successfully";
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

    [HttpDelete("{id}")]
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
