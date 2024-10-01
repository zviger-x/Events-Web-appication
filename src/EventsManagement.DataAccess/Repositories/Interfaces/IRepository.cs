using EventsManagement.DataObjects.Entities.Interfaces;
using EventsManagement.DataObjects.Utilities.Interfaces;

namespace EventsManagement.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
        where T : IEntity
    {
        /// <summary>
        /// Creates an entity into the data table.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        Task CreateAsync(T entity);

        /// <summary>
        /// Updates an entity in the data table.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Removes an entity in the data table.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Returns an entity by its id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Entity.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Returns an array of all entities in the data table.
        /// </summary>
        /// <returns>An array of all entities in the data table.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Returns a paginated list of elements
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        Task<IPaginatedList<T>> GetPaginatedListAsync(int pageIndex, int pageSize);

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        Task SaveChangesAsync();
    }
}
