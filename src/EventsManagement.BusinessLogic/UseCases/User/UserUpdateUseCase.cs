using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;
using Microsoft.AspNet.Identity;

namespace EventsManagement.BusinessLogic.UseCases.UserUseCases
{
    internal class UserUpdateUseCase : BaseUseCase<UserDTO>, IUpdateUseCase<UserDTO>
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator, IPasswordHasher passwordHasher)
            : base(unitOfWork, mapper, validator)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task Execute(UserDTO entity)
        {
            entity.IsUpdate = true;

            await _validator.ValidateAndThrowAsync(entity);

            entity.Password = _passwordHasher.HashPassword(entity.Password);

            var u = _mapper.Map<User>(entity);
            _unitOfWork.UserRepository.Update(u);
            await _unitOfWork.UserRepository.SaveChangesAsync();
        }
    }
}
