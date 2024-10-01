using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();

            CreateMap<EventUser, EventUserDTO>();
            CreateMap<EventUserDTO, EventUser>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
