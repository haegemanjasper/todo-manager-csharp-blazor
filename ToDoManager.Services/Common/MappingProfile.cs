using AutoMapper;
using ToDoManager.Shared.ToDoItems;
using ToDoManager.Domain.ToDoItems;

namespace ToDoManager.Services.Common;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItem, ToDoItemDto.Index>();
            CreateMap<ToDoItemDto.Create, ToDoItem>();
            CreateMap<ToDoItemDto.Edit, ToDoItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
            CreateMap<ToDoItem, ToDoItemDto.Detail>()
                .ForMember(dest => dest.Title, t => t.MapFrom(src => src.Title));
        }
    }