namespace ToDoManager.Shared.ToDoItems;
    public  interface IToDoItemService
    {
    Task<List<ToDoItemDto.Index>> GetIndexAsync();
    Task<ToDoItemDto.Index> CreateAsync(ToDoItemDto.Create toDoItemDto);
    Task<ToDoItemDto.Detail> GetDetailAsync(int id);
    Task EditAsync(int id, ToDoItemDto.Edit dto);
    Task DeleteAsync(int id);

   }
