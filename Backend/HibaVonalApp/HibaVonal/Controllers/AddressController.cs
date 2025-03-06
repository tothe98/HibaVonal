using Hibavonal.DataContext.Entities;
using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using HibaVonal.DataContext.Dtos;

namespace HibaVonal.Controllers
{
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
        public async Task<IEnumerable<Address>> List()
        {
            return await _addressService.List();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddressDto address)
        {
            APIResponse response = new APIResponse();
            try
            {
                await _addressService.Add(address);
                response.StatusCode = 200;
                response.Message = "Address added successfully";
                return Ok(response);
            }
            catch (MandatoryPropertyEmptyException ex)
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
        public async Task<IActionResult> Update(int id, [FromBody] AddressDto address)
        {
            APIResponse response = new APIResponse();
            try
            {
                await _addressService.Update(id, address);
                response.StatusCode = 200;
                response.Message = "Address updated successfully";
                return Ok(response);
            }
            catch(AddressWithIdNotExistsException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch(MandatoryPropertyEmptyException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                await _addressService.Delete(id);
                response.StatusCode = 200;
                response.Message = "Address deleted successfully";
                return Ok(response);
            }
            catch (AddressWithIdNotExistsException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            return BadRequest(response);
        }
    }
}
