using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataObjects.Entities;
using Microsoft.AspNet.Identity;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserCreateUseCase : BaseUseCase<UserDTO>, ICreateUseCase<UserDTO>
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserCreateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator, IPasswordHasher passwordHasher)
            : base(unitOfWork, mapper, validator)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task CreateAsync(UserDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            entity.Password = _passwordHasher.HashPassword(entity.Password);

            var u = _mapper.Map<User>(entity);
            await _unitOfWork.UserRepository.CreateAsync(u);
            await _unitOfWork.UserRepository.SaveChangesAsync();
        }
    }
}
