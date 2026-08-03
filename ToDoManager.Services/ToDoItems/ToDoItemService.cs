using AutoMapper;
using FluentValidation;
using ToDoManager.Shared.ToDoItems;
using ToDoManager.Persistence;
using ToDoManager.Domain.ToDoItems;
using ToDoManager.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace ToDoManager.Services.ToDoItems;
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

    public async Task<List<ToDoItemDto.Index>> GetIndexAsync()
    {
        var toDoItems = await _dbContext.ToDoItems.ToListAsync();
        return _mapper.Map<List<ToDoItemDto.Index>>(toDoItems);
    }

    public async Task<ToDoItemDto.Detail> GetDetailAsync(int id)
    {
        ToDoItem? toDoItem = await _dbContext.ToDoItems.FindAsync(id);
        if (toDoItem is null)
            throw new EntityNotFoundException(nameof(toDoItem), id);
        return _mapper.Map<ToDoItemDto.Detail>(toDoItem);
    }

    public async Task<ToDoItemDto.Index> CreateAsync(ToDoItemDto.Create toDoItemDto)
    {
        await _createValidator.ValidateAndThrowAsync(toDoItemDto);

        if (await _dbContext.ToDoItems.AnyAsync(t => t.Title == toDoItemDto.Title))
            throw new EntityAlreadyExistsException(nameof(ToDoItem), nameof(ToDoItem.Title), nameof(toDoItemDto.Title));

        var toDoItem = _mapper.Map<ToDoItem>(toDoItemDto);

        await _dbContext.ToDoItems.AddAsync(toDoItem);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ToDoItemDto.Index>(toDoItem);
    }

    public async Task EditAsync(int id, ToDoItemDto.Edit toDoItemDto)
    {
        await _editValidator.ValidateAndThrowAsync(toDoItemDto);

        ToDoItem? toDoItem = await _dbContext.ToDoItems.FindAsync(id);
        if (toDoItem is null)
            throw new EntityNotFoundException(nameof(toDoItem), id);

        if (await _dbContext.ToDoItems.Where(t => t.Id != id).AnyAsync(t => t.Title == toDoItemDto.Title))
            throw new EntityAlreadyExistsException(nameof(ToDoItem), nameof(ToDoItem.Title), toDoItemDto.Title);

        _mapper.Map(toDoItemDto, toDoItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        ToDoItem? toDoItem = await _dbContext.ToDoItems.FindAsync(id);
        if (toDoItem is null)
            throw new EntityNotFoundException(nameof(toDoItem), id);

        await _dbContext.ToDoItems.Where(t => t.Id == id).ExecuteDeleteAsync();
    }
}