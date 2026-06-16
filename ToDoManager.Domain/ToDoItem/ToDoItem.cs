using Ardalis.GuardClauses;
using ToDoManager.Domain.Common;
using ToDoManager.Domain.Common.Enums;

namespace ToDoManager.Domain.ToDoItem;

public class ToDoItem : Entity
{

    private string _title = default!;
    public required string Title 
    {
        get => _title;
        set => Guard.Against.NullOrWhiteSpace(value); 
    }

    private Priority _priority = default!;
    public Priority Priority 
    {   
        get => _priority; 
        set => Guard.Against.EnumOutOfRange(value); 
    }
}
