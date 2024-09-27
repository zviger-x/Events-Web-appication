using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
        where T : IEntity
    {
        /// <summary>
        /// Creates an entity into the data table.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        Task Create(T entity);

        /// <summary>
        /// Updates an entity in the data table.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        Task Update(T entity);

        /// <summary>
        /// Removes an entity in the data table.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        Task Delete(T entity);

        /// <summary>
        /// Returns an entity by its id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Entity.</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Returns an array of all entities in the data table.
        /// </summary>
        /// <returns>An array of all entities in the data table.</returns>
        IQueryable<T> GetAll();
    }
}
