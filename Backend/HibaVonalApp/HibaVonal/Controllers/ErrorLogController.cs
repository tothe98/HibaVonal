using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HibaVonal.DataContext;
using HibaVonal.Services.Services;
using HibaVonal.DataContext.Dtos;
using LibraryCommon.Models;
using HibaVonal.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace HibaVonal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorLogsController : ControllerBase
    {
        private readonly IErrorLogService _errorLogService;

        public ErrorLogsController(IErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        [HttpGet]
        [Authorize("MaintenanceManager")]
        public async Task<ActionResult<List<ErrorLogDto>>> List()
        {
            return await _errorLogService.List();
        }

        [HttpPost]
        [Authorize("User")]
        public async Task<ActionResult<ErrorLogDto>> Create([FromBody] ErrorLogCreateDto errorLogCreateDto)
        {
            APIResponse response = new APIResponse();
            try
            {
                var result = await _errorLogService.Create(errorLogCreateDto);
                response.Data = result;
                response.StatusCode = 200;
                response.Message = "Error log created successfully.";
                return Ok(response);
            }
            catch (ReporterWithIdNotExistsException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (MaintenanceWorkerWithIdNotExistsException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.InnerException?.Message;
            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        [Authorize("MaintenanceWorker")]
        public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] ErrorLogCreateDto errorLogUpdateDto)
        {
            APIResponse response = new APIResponse();
            try
            {
                var result = await _errorLogService.Update(id, errorLogUpdateDto);
                response.Data = result;
                response.StatusCode = 200;
                response.Message = "Error log updated successfully.";
                return Ok(response);
            }
            catch (ErrorLogWithIdNotExistsException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (ReporterWithIdNotExistsException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (MaintenanceWorkerWithIdNotExistsException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.InnerException?.Message;
            }

            return BadRequest(response);
        }



        [HttpDelete("{id}")]
        [Authorize("MaintenanceManager")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                await _errorLogService.Delete(id);
                response.StatusCode = 200;
                response.Message = "Error log deleted successfully.";
                return Ok(response);
            }
            catch (ErrorLogWithIdNotExistsException ex)
            {
                response.StatusCode = 202;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.InnerException?.Message;
            }

            return BadRequest(response);
        }


    }
}
