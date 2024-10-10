using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.User
{
    /// <summary>
    /// Checks the user's email and password.
    /// Returns the user data or null if the data is invalid.
    /// </summary>
    public interface IVerifyLoginDataUseCase : IUseCase<LoginRequest, Task<UserDTO>>
    {
    }
}
