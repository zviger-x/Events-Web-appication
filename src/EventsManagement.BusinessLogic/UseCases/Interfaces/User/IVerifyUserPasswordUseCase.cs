using EventsManagement.BusinessLogic.Services.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.User
{
    /// <summary>
    /// Checks the user's password.
    /// Returns true if the password is correct.
    /// </summary>
    public interface IVerifyUserPasswordUseCase : IUseCase<(string userPassword, string password), bool>
    {
    }
}
