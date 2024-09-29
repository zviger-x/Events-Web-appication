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
        Task<Event> GetByMameAsync(string name);

        /// <summary>
        /// Returns an array of events by date.
        /// </summary>
        /// <param name="date">Event date and time.</param>
        /// <returns>An array of events.</returns>
        IQueryable<Event> GetByDateAsync(DateTime date);

        /// <summary>
        /// Returns an array of events by venue.
        /// </summary>
        /// <param name="venue">Event venue.</param>
        /// <returns>An array of events.</returns>
        IQueryable<Event> GetByVenueAsync(string venue);

        /// <summary>
        /// Returns an array of events by category.
        /// </summary>
        /// <param name="category">Event category.</param>
        /// <returns>An array of events.</returns>
        IQueryable<Event> GetByCategoryAsync(string category);
    }
}
