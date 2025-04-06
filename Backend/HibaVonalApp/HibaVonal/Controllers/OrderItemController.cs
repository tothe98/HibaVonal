using Hibavonal.DataContext.Entities;
using HibaVonal.Services.Services;
using HibaVonal.Services.Exceptions;
using LibraryCommon.Models;
using Microsoft.AspNetCore.Mvc;
using HibaVonal.DataContext.Dtos;

namespace HibaVonal.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;
    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderItem>> List()
    {
        return await _orderItemService.List();
    }

    /*[HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderItemDto orderItem)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _orderItemService.Create(orderItem);
            response.StatusCode = 200;
            response.Message = "OrderItem added successfully";
            return Ok(response);
        }
        catch (OrderWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (EquipmentWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
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
    public async Task<IActionResult> Update(int id , OrderItemDto orderItem)
    {
        APIResponse response = new APIResponse();
        try
        {
            await _orderItemService.Update(id ,orderItem);
            response.StatusCode = 200;
            response.Message = "OrderItem updated successfully";
            return Ok(response);
        }
        catch (OrderWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (EquipmentWithIdNotExistsException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (MandatoryPropertyEmptyException ex)
        {
            response.StatusCode = 202;
            response.Message = ex.Message;
        }
        catch (OrderItemWithIdNotExistsException ex)
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
            await _orderItemService.Delete(id);
            response.StatusCode = 200;
            response.Message = "OrderItem deleted successfully";
            return Ok(response);
        }
        catch (OrderItemWithIdNotExistsException ex)
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
    }*/
}
