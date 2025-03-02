using Hibavonal.DataContext.Entities;
using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HibaVonal.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> List()
        {
            return await _orderService.List();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Order order)
        {
            APIResponse response = new APIResponse();
            try
            {
                await _orderService.Add(order);
                response.StatusCode = 200;
                response.Message = "Order added successfully";
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
        public async Task<IActionResult> Update(Order order)
        {
            APIResponse response = new APIResponse();
            try
            {
                await _orderService.Update(order);
                response.StatusCode = 200;
                response.Message = "Order updated successfully";
                return Ok(response);
            }
            catch (MandatoryPropertyEmptyException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (OrderWithIdNotExistsException ex)
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
                await _orderService.Delete(id);
                response.StatusCode = 200;
                response.Message = "Order deleted successfully";
                return Ok(response);
            }
            catch (OrderWithIdNotExistsException ex)
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
}
