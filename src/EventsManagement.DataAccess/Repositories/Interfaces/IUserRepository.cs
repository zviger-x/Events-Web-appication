using EventsManagement.DataObjects.Entities;

namespace EventsManagement.DataAccess.Repositories.Interfaces
{
    internal interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Returns an user by its email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>User.</returns>
        Task<User> GetByEmailAsync(string email);
    }
}
