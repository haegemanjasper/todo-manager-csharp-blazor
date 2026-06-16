using ToDoManager.Shared.Common.Enums;

namespace ToDoManager.Shared.ToDoItems
{
    public abstract class ToDoItemDto
    {
        public class Index
        {
            public int Id { get; set; }
            public required string Title { get; set; }
            public required Priority Priority { get; set; }
        }
    }
}
