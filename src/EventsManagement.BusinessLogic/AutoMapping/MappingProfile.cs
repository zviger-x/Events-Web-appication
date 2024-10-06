using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.AutoMapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDTO>().AfterMap<EventUserCounterMappingAction>();
            CreateMap<EventDTO, Event>();

            CreateMap<EventUser, EventUserDTO>();
            CreateMap<EventUserDTO, EventUser>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
