using EventsManagement.DataObjects.Entities;

namespace EventsManagement.DataAccess.Repositories.Interfaces
{
    internal interface IEventUserRepository : IRepository<EventUser>
    {
        /// <summary>
        /// Registers a user in an event
        /// </summary>
        /// <param name="eventUser">User to register.</param>
        /// <returns></returns>
        Task RegisterUserInEventAsync(EventUser eventUser);

        /// <summary>
        /// Unregisters a user in an event
        /// </summary>
        /// <param name="eventUser">User of the event.</param>
        /// <returns></returns>
        void UnregisterUserInEvent(EventUser eventUser);

        /// <summary>
        /// Returns all users of the event.
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>An array of all user of the event.</returns>
        IQueryable<EventUser> GetUsersOfEventAsync(int eventId);
    }
}
