using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetUserByEmailUseCase
    {
        /// <summary>
        /// Returns an user by its email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Event.</returns>
        Task<UserDTO> GetByEmailAsync(string email);
    }
}
