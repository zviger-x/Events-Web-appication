using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserDeleteUseCase : BaseUseCase<UserDTO>, IDeleteUseCase<UserDTO>
    {
        public UserDeleteUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task Execute(UserDTO entity)
        {
            // Главное, чтобы id совпадал
            // await _validator.ValidateAndThrowAsync(entity);

            var u = _mapper.Map<User>(entity);
            _unitOfWork.UserRepository.Delete(u);
            await _unitOfWork.UserRepository.SaveChangesAsync();
        }
    }
}
