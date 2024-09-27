using EventsManagement.DataObjects.Entities;

namespace EventsManagement.DataAccess.Repositories.Interfaces
{
    internal interface IEventRepository : IRepository<Event>
    {
        /// <summary>
        /// Returns an event by its name.
        /// </summary>
        /// <param name="name">Event name.</param>
        /// <returns>Event.</returns>
        Task<Event> GetByMame(string name);
    }
}
