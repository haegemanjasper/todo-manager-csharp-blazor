using AutoMapper;
using FluentValidation;
using ToDoManager.Shared.ToDoItems;
using ToDoManager.Persistence;
using ToDoManager.Domain.ToDoItems;
using ToDoManager.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace ToDoManager.Services.ToDoItems
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IValidator<ToDoItemDto.Create> _createValidator;
        private readonly IValidator<ToDoItemDto.Edit> _editValidator;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ToDoItemService(IValidator<ToDoItemDto.Create> createValidator, IValidator<ToDoItemDto.Edit> editValidator, ApplicationDbContext dbContext, IMapper mapper)
        {
            _createValidator = createValidator;
            _editValidator = editValidator;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task EditAsync(int id, ToDoItemDto.Edit toDoItemDto)
        {
            await _editValidator.ValidateAndThrowAsync(toDoItemDto);

            ToDoItem? toDoItem = await _dbContext.ToDoItems.FindAsync(id);
            if (toDoItem is null)
                throw new EntityNotFoundException(nameof(toDoItem), id);

            if (await _dbContext.ToDoItems.Where(t => t.Id != id).AnyAsync(t => t.Title == toDoItemDto.Title))
                throw new EntityAlreadyExistsException(nameof(ToDoItem), nameof(ToDoItem.Title), toDoItemDto.Title);
        }

    }
}
