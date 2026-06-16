using Ardalis.GuardClauses;
using ToDoManager.Domain.Common;
using ToDoManager.Shared.Common.Enums;

namespace ToDoManager.Domain.ToDoItems;

public class ToDoItem : Entity
{

    private string _title = default!;
    public required string Title 
    {
        get => _title;
        set => _title = Guard.Against.NullOrWhiteSpace(value); 
    }

    private Priority _priority = default!;
    public Priority Priority 
    {   
        get => _priority; 
        set => _priority = Guard.Against.EnumOutOfRange(value); 
    }
    public bool IsCompleted { get; set; }
}

