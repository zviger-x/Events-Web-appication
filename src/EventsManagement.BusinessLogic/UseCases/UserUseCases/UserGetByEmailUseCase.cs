using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.User;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.UseCases.UserUseCases
{
    internal class UserGetByEmailUseCase : BaseUseCase<UserDTO>, IGetUserByEmailUseCase
    {
        public UserGetByEmailUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<UserDTO> Execute(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), StandartValidationMessages.ParameterIsNullOrEmpty);

            var e = await _unitOfWork.UserRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDTO>(e);
        }
    }
}
