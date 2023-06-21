using Microsoft.AspNetCore.Mvc;
using ToDoList.Dto;
using ToDoList.Service.Interface;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {
        private IToDoListService _service;

        public ToDoListController(IToDoListService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginationAsync([FromHeader] Guid userId, [FromQuery] int skip = 0, [FromQuery] int take = 5)
        {
            if (userId == Guid.Empty) return Forbid();
            return Ok(await _service.GetPaginationAsync(userId, skip, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromHeader] Guid userId, [FromRoute] Guid id)
        {
            if (userId == Guid.Empty) return Forbid();
            return Ok(await _service.GetByIdAsync(userId, id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromHeader] Guid userId, [FromBody] ToDoListDto toDoList)
        {
            if (userId == Guid.Empty) return Forbid();
            return Created("/", await _service.InsertAsync(userId, toDoList));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromHeader] Guid userId, [FromBody] ToDoListDto toDoList)
        {
            if (userId == Guid.Empty) return Forbid();
            return Ok(await _service.UpdateAsync(userId, toDoList));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromHeader] Guid userId, [FromRoute] Guid id)
        {
            if (userId == Guid.Empty) return Forbid();
            await _service.DeleteAsync(userId, id);
            return Ok();
        }
    }
}