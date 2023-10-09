using ChatCore.DTO;
using ChatCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserDTO model)
        {
            try
            {
                var userId = await _userService.AddUserAsync(model);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
                throw;
            }
        }
    }
}
