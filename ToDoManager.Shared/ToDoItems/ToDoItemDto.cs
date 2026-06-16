using ToDoManager.Shared.Common.Enums;
using FluentValidation;

namespace ToDoManager.Shared.ToDoItems
{
    public abstract class ToDoItemDto
    {
        public class Index
        {
            public int Id { get; set; }
            public required string Title { get; set; }
            public required Priority Priority { get; set; }
            public bool IsCompleted { get; set; }
        }

        public class Detail
        {
            public int Id { get; set; }
            public required string Title { get; set; }
            public required Priority Priority { get; set; }
            public bool IsCompleted { get; set; }

        }

        public class Create
        {
            public required string Title { get; set; }
            public required Priority Priority { get; set; }

            public class Validator : AbstractValidator<Create>
            {
                public Validator()
                {
                    RuleFor(t => t.Title).NotEmpty().WithMessage("Title is required.");
                    RuleFor(t => t.Priority).IsInEnum().When(t => t.Priority != null).WithMessage("Priority should be a valid option.");
                }
            }
        }

        public class Edit
        {
            public string? Title { get; set; }
            public Priority? Priority { get; set; }
            public bool IsCompleted { get; set; }

            public class Validator : AbstractValidator<Edit>
            {
                public Validator()
                {
                    RuleFor(t => t.Title).NotEmpty().WithMessage("Title cannot be empty.");
                    RuleFor(t => t.Priority).IsInEnum().WithMessage("Priority should be in Enum.");
                }
            }
        }
    }
}
