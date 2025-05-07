using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using HibaVonal.Services.Services;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize("Admin")]
    public class RoomEquipmentController : ControllerBase
    {
        private readonly IRoomEquipmentService _roomEquipmentService;
        public RoomEquipmentController(IRoomEquipmentService roomEquipmentService)
        {
            _roomEquipmentService = roomEquipmentService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RoomDto>>> List()
        {
            return Ok(await _roomEquipmentService.List());
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<ActionResult<APIResponse>> Create([FromBody] RoomEquipmentCreateDto roomEquipment)
        {
            try
            {
                RoomEquipmentDto result = await _roomEquipmentService.CreateRoomEquipment(roomEquipment);
                return Ok(new APIResponse
                {
                    Data = result,
                    StatusCode = 200,
                    Message = "RoomEquipment added successfully"
                });
            }

            catch (EquipmentWithIdNotExistsException ex)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Message = ex.Message
                });
            }
            catch (RoomWithIdNotExistsException ex)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Message = ex.Message
                });
            }
            catch (RoomEquipmentAlreadyExistsException ex)
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

        [HttpDelete]
        [Authorize("Admin")]
        public async Task<ActionResult<APIResponse>> Delete(int RoomId, int Equipment)
        {
            try
            {
                await _roomEquipmentService.Delete(RoomId , Equipment);
                return Ok(new APIResponse
                {
                    StatusCode = 200,
                    Message = "RoomEquipment deleted successfully"
                });
            }
            catch (RoomWithIdNotExistsException ex)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Message = ex.Message
                });
            }
            catch (EquipmentWithIdNotExistsException ex)
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
        [HttpPut]
        //[Authorize("Admin")]
        public async Task<ActionResult<APIResponse>> Update(int roomId, int equipment, [FromBody] RoomEquipmentUpdateDto roomEquipment)
        {
            try
            {
                RoomEquipmentDto result = await _roomEquipmentService.UpdateRoomEquipment(roomId, equipment , roomEquipment);
                return Ok(new APIResponse
                {
                    Data = result,
                    StatusCode = 200,
                    Message = "RoomEquipment updated successfully"
                });
            }
            catch (RoomWithIdNotExistsException ex)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Message = ex.Message
                });
            }
            catch (EquipmentWithIdNotExistsException ex)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = 404,
                    Message = ex.Message
                });
            }

            catch (RoomEquipmentWithIdsNotExistsException ex)
            {
                return NotFound(new APIResponse
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
    }
}
