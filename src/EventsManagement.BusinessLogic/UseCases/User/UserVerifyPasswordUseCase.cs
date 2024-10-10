using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.User;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using Microsoft.AspNet.Identity;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserVerifyPasswordUseCase : BaseUseCase<UserDTO>, IVerifyUserPasswordUseCase
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserVerifyPasswordUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator, IPasswordHasher passwordHasher)
            : base(unitOfWork, mapper, validator)
        {
            _passwordHasher = passwordHasher;
        }

        public bool Execute((string userPassword, string password) request)
        {
            var result = _passwordHasher.VerifyHashedPassword(request.userPassword, request.password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
