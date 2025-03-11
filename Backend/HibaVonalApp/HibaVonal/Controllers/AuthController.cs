using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using HibaVonal.Services.Services;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HibaVonal.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginRequest([FromBody] LoginDto loginData)
        {
            APIResponse response = new APIResponse();
            try
            {
                AccessTokenDto accessTokenDto = await _authService.Login(loginData);
                response.StatusCode = 200;
                response.Message = "Login successful";
                response.Data = accessTokenDto;
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
            catch (UnauthorizedAccessException ex)
            {
                response.StatusCode = 401;
                response.Message = "Unauthorized";
                return Unauthorized(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] RegisterDto register)
        {
            APIResponse response = new APIResponse();
            try
            {
                UserDataDto user = await _authService.Registration(register);
                response.StatusCode = 200;
                response.Message = "Registration successful";
                response.Data = user;
                return Ok(response);
            }
            catch (MandatoryPropertyEmptyException ex)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (UserAlreadyExistsException ex)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
                return BadRequest(response);
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

        [HttpGet]
        [Authorize("User")]
        public IActionResult ValidateToken([FromHeader] string authorization)
        {

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {

                var scheme = headerValue.Scheme;
                var parameter = headerValue.Parameter;

                Console.WriteLine(parameter);

                bool isValid = _authService.ValidatekToken(parameter);
                if (isValid)
                {
                    return NoContent();
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }


        }
    }

}

