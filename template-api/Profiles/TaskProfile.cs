using AutoMapper;
using Backend.Dtos;

namespace Backend.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            // Source -> Target
            CreateMap<TaskCreateDto, Backend.Models.Task>();
            CreateMap<Backend.Models.Task, TaskReadDto>();
            CreateMap<TaskUpdateDto, Backend.Models.Task>();
        }
    }
}