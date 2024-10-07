using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
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

        public bool VerifyPassword(string userPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(userPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
