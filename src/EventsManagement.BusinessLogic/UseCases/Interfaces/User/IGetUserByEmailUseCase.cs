using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.User
{
    /// <summary>
    /// Returns an user by its email.
    /// </summary>
    public interface IGetUserByEmailUseCase : IUseCase<string, Task<UserDTO>>
    {
    }
}
