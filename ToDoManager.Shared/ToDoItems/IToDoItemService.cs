namespace ToDoManager.Shared.ToDoItems;
    public  interface IToDoItemService
    {
    Task<ToDoItemDto.Index> CreateAsync(ToDoItemDto.Create toDoItemDto);
    Task<ToDoItemDto.Detail> GetDetailAsync(int id);
    Task EditAsync(int id, ToDoItemDto.Edit dto);
    Task DeleteAsync(int id);

   }
