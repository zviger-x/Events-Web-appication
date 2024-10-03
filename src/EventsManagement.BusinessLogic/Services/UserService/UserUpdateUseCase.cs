using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserUpdateUseCase : BaseUseCase<UserDTO>, IUpdateUseCase<UserDTO>
    {
        public UserUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task UpdateAsync(UserDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            var u = _mapper.Map<User>(entity);
            _unitOfWork.UserRepository.Update(u);
            await _unitOfWork.UserRepository.SaveChangesAsync();
        }
    }
}
