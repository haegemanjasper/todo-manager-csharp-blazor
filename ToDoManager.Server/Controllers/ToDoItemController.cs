using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoManager.Domain.ToDoItems;
using ToDoManager.Shared.ToDoItems;


namespace ToDoManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ToDoItemController : ControllerBase
{
    private readonly IToDoItemService _toDoItemService;

    public ToDoItemController(IToDoItemService toDoItemService)
    {
        _toDoItemService = toDoItemService;
    }

    [SwaggerOperation("Returns a list of toDoItems.")]
    [HttpGet]
    public async Task<ActionResult<List<ToDoItemDto.Index>>> GetIndex()
    {
        var items = await _toDoItemService.GetIndexAsync();
        return Ok(items);
    }

    [SwaggerOperation("Returns a specific toDoItem.")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemDto.Detail>> GetDetail(int id)
    {
        ToDoItemDto.Detail item = await _toDoItemService.GetDetailAsync(id);
        return Ok(item);
    }

    [SwaggerOperation("Creates a new item and returns it.")]
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] ToDoItemDto.Create dto)
    {
        ToDoItemDto.Index item = await _toDoItemService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetDetail), new { item.Id }, item);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Edit(int id, [FromBody] ToDoItemDto.Edit dto)
    {
        await _toDoItemService.EditAsync(id, dto);
        return NoContent();
    }

    [SwaggerOperation("Removes an existing item.")]
    [HttpDelete("{id}")]
    public async Task<NoContentResult> Delete(int id)
    {
        await _toDoItemService.DeleteAsync(id);
        return NoContent();
    }
}
