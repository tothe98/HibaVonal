using HibaVonal.DataContext.Dtos;
using HibaVonal.DataContext.Entities;
using HibaVonal.Services.Exceptions;
using HibaVonal.Services.Services;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<UserDataDto>> List()
        {
            return await _userService.List();

        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.StatusCode = 200;
                response.Data = await _userService.GetById(id);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.StatusCode = 404;
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetByEmail(string email)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.StatusCode = 200;
                response.Data = await _userService.GetByEmail(email);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.StatusCode = 404;
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                return StatusCode(500, response);

            }
        }

        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UserUpdateDto user)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.StatusCode = 201;
                response.Message = "Update is successful";
                response.Data = await _userService.Update(user);
                return Ok(response);
            }
            catch (MandatoryPropertyEmptyException ex)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
                return BadRequest(response);

            }
            catch (InvalidPropertyValueException ex)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (InvalidEmailDomainException ex)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (NotFoundException ex)
            {
                response.StatusCode = 404;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                return StatusCode(500, response);

            }
        }

        [HttpDelete]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                await _userService.Delete(id);
                response.StatusCode = 204;
                response.Message = "Delete is successful";
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.StatusCode = 404;
                response.Message = ex.Message;
                return NotFound(response);
            } catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
