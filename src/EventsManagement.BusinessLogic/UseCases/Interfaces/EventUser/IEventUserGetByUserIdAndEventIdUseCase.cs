using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser
{
    /// <summary>
    /// Returns the EventUserDTO by the user ID and event ID.
    /// </summary>
    public interface IEventUserGetByUserIdAndEventIdUseCase : IUseCase<(int userId, int eventId), Task<EventUserDTO>>
    {
    }
}
