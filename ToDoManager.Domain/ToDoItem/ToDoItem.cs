using System.Runtime.Versioning;
using ToDoManager.Domain.Common;

namespace ToDoManager.Domain.ToDoItem;

public class ToDoItem : Entity
{

    private string _title = default!;
    public required string Title 
    {
        get => _title;
        set => _title; 
    }
}
