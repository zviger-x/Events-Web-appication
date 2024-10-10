using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser
{
    /// <summary>
    /// Returns all users of the event as EventUserDTO.
    /// Requests an event ID.
    /// </summary>
    public interface IGetUsersOfEventUseCase : IUseCase<int, IQueryable<EventUserDTO>>
    {
    }
}
