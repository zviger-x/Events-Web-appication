using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser
{
    /// <summary>
    /// Returns all events where the user participates.
    /// Requests a user ID.
    /// </summary>
    public interface IGetEventsOfUserUseCase : IUseCase<int, IEnumerable<EventDTO>>
    {
    }
}
