using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.AutoMapping
{
    internal class EventUserCounterMappingAction : IMappingAction<Event, EventDTO>
    {
        private readonly IGetUsersOfEventUseCase _getUsersOfEventUseCase;
    
        public EventUserCounterMappingAction(IGetUsersOfEventUseCase getUsersOfEventUseCase)
        {
            _getUsersOfEventUseCase = getUsersOfEventUseCase;
        }
    
        public void Process(Event source, EventDTO destination, ResolutionContext context)
        {
            var eventUsers = _getUsersOfEventUseCase.Execute(source.Id).ToList();

            destination.CurrentNumberOfParticipants = eventUsers.Count;
        }
    }
}
