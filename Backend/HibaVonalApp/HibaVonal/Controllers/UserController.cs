using HibaVonal.DataContext.Entities;
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
        public async Task<IEnumerable<User>> List()
        {
            return await _userService.List();

        }
    }
}
