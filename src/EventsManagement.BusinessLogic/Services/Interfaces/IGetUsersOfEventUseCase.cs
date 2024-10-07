using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetUsersOfEventUseCase
    {
        /// <summary>
        /// Returns all users of the event.
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>An array of all user of the event.</returns>
        IQueryable<EventUserDTO> GetUsersOfEvent(int eventId);
    }
}
