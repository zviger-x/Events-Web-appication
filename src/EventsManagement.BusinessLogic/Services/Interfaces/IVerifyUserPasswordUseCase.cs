using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IVerifyUserPasswordUseCase
    {
        /// <summary>
        /// Checks the user's password.
        /// </summary>
        /// <param name="user">User current password</param>
        /// <param name="password">Password</param>
        /// <returns>true if the password is correct</returns>
        bool VerifyPassword(string userPassword, string password);
    }
}
