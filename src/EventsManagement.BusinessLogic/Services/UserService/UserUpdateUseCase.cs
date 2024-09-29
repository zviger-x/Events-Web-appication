using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserUpdateUseCase : BaseUseCase<UserDTO>, IUpdateUseCase<UserDTO>
    {
        public UserUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task Update(UserDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            var u = _mapper.Map<User>(entity);
            await _unitOfWork.UserRepository.Update(u);
        }
    }
}
