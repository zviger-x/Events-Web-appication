using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.User;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.UseCases.UserUseCases
{
    internal class UserVerifyLoginDataUseCase : BaseUseCase<UserDTO>, IVerifyLoginDataUseCase
    {
        private readonly IGetUserByEmailUseCase _getUserByEmailUseCase;
        private readonly IVerifyUserPasswordUseCase _verifyUserPasswordUseCase;

        public UserVerifyLoginDataUseCase(IUnitOfWork unitOfWork,
            IMapper mapper,
            IBaseValidator<UserDTO> validator,
            IGetUserByEmailUseCase getUserByEmailUseCase,
            IVerifyUserPasswordUseCase verifyUserPasswordUseCase) : base(unitOfWork, mapper, validator)
        {
            _getUserByEmailUseCase = getUserByEmailUseCase;
            _verifyUserPasswordUseCase = verifyUserPasswordUseCase;
        }

        public async Task<UserDTO> Execute(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentException(UserValidationMessages.InvalidEmailOrPassword);
            }

            var user = await _getUserByEmailUseCase.Execute(request.Email);
            if (user == null)
            {
                throw new ArgumentException(UserValidationMessages.InvalidEmailOrPassword);
            }

            bool isPasswordValid = _verifyUserPasswordUseCase.Execute((user.Password, request.Password));
            if (!isPasswordValid)
            {
                throw new ArgumentException(UserValidationMessages.InvalidEmailOrPassword);
            }

            return user;
        }
    }
}
