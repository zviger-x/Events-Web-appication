using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserCreateUseCase : BaseUseCase<UserDTO>, ICreateUseCase<UserDTO>
    {
        public UserCreateUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task CreateAsync(UserDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            var u = _mapper.Map<User>(entity);
            await _unitOfWork.UserRepository.CreateAsync(u);
            await _unitOfWork.UserRepository.SaveChangesAsync();
        }
    }
}
