using EventsManagement.DataObjects.Entities;

namespace EventsManagement.DataAccess.Repositories.Interfaces
{
    internal interface IEventUserRepository : IRepository<EventUser>
    {
        /// <summary>
        /// Registers a user in an event
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="eventId">Event id</param>
        /// <param name="registrationDate">Date of registration</param>
        /// <returns></returns>
        Task RegisterUserInEvent(int userId, int eventId, DateTime registrationDate);

        /// <summary>
        /// Unregisters a user in an event
        /// </summary>
        /// <param name="eventUser">User of the event.</param>
        /// <returns></returns>
        Task UnregisterUserInEvent(EventUser eventUser);

        /// <summary>
        /// Returns all users of the event.
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>An array of all user of the event.</returns>
        IQueryable<EventUser> GetUsersOfEvent(int eventId);
    }
}
