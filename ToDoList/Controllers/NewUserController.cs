using Microsoft.AspNetCore.Mvc;
using ToDoList.Service.Interface;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewUserController : ControllerBase
    {
        private readonly IUserService _userService;

        public NewUserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Created("/", await _userService.NewUserAsync());
        }
    }
}
