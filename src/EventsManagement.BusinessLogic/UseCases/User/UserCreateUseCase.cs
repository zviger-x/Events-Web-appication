using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;
using Microsoft.AspNet.Identity;

namespace EventsManagement.BusinessLogic.UseCases.UserUseCases
{
    internal class UserCreateUseCase : BaseUseCase<UserDTO>, ICreateUseCase<UserDTO>
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserCreateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator, IPasswordHasher passwordHasher)
            : base(unitOfWork, mapper, validator)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task Execute(UserDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            entity.Password = _passwordHasher.HashPassword(entity.Password);

            var u = _mapper.Map<User>(entity);
            await _unitOfWork.UserRepository.CreateAsync(u);
            await _unitOfWork.UserRepository.SaveChangesAsync();
        }
    }
}
