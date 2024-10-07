using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetEventsOfUserUseCase
    {
        /// <summary>
        /// Returns all events where the user participates.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>An array of all events of the user.</returns>
        IEnumerable<EventDTO> GetEventsOfUser(int userId);
    }
}
