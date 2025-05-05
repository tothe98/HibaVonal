using HibaVonal.DataContext.Dtos;
using HibaVonal.DataContext.Entities;
using HibaVonal.Services.Exceptions;
using HibaVonal.Services.Services;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize("Admin")]
        public async Task<List<UserDataDto>> List()
        {
            return await _userService.List();

        }

        [HttpGet("{id}")]
        [Authorize("User")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.StatusCode = 200;
                response.Data = await _userService.GetById(id);
                return Ok(response);
            }
            catch (NotFoundException e)
            {
                response.StatusCode = 404;
                response.Message = e.Message;
                return NotFound(response);
            }
            catch (Exception e)
            {
                response.StatusCode = 500;
                response.Message = e.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        [Authorize("User")]
        public async Task<ActionResult<APIResponse>> GetCurrent()
        {
            APIResponse response = new APIResponse();
            try
            {
                var id = User.FindFirst("id")?.Value;
                if (string.IsNullOrEmpty(id))
                {
                    response.StatusCode = 401;
                    response.Message = "Unauthorized";
                    return Unauthorized(response);
                }

                var user = await _userService.GetById(int.Parse(id));
                if (user == null)
                {
                    throw new NotFoundException("User not found");
                }

                response.StatusCode = 200;
                response.Data = new
                {
                    id,
                    name = user.Name,
                    email = user.Email,
                    roles = user.Roles
                };
                return Ok(response);
            }
            catch (NotFoundException e)
            {
                response.StatusCode = 404;
                response.Message = e.Message;
                return NotFound(response);
            }
            catch (Exception e)
            {
                response.StatusCode = 500;
                response.Message = e.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{email}")]
        [Authorize("User")]
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
        [Authorize]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UserUpdateDto user)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.StatusCode = 201;
                response.Message = "Update is successful";
                var id = int.Parse(User.FindFirst("id")?.Value);
                response.Data = await _userService.Update(id, user);
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

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<APIResponse>> PasswordChange([FromBody] PasswordChangeDto userpassworddto)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.StatusCode = 201;
                response.Message = "Password change is successful";
                var id = int.Parse(User.FindFirst("id")?.Value);
                response.Data = await _userService.PasswordChange(id, userpassworddto);
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
            catch (NotFoundException ex)
            {
                response.StatusCode = 404;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                response.StatusCode = 401;
                response.Message = "Unauthorized";
                return Unauthorized(response);
            }
            catch (PasswordsNotMatchException ex)
            {
                response.StatusCode = 400;
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

        [HttpDelete("{id}")]
        [Authorize("Admin")]
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
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
