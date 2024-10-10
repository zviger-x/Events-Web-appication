using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces
{
    public interface IGenerateJwtTokenUseCase
        : IUseCase<(UserDTO user, string secretKey, string issuer, string audience), string>
    {
    }
}
