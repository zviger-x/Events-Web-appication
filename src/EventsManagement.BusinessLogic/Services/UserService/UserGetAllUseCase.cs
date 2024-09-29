using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserGetAllUseCase : BaseUseCase<UserDTO>, IGetAllUseCase<UserDTO>
    {
        public UserGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<UserDTO> GetAllAsync()
        {
            var users = _unitOfWork.UserRepository.GetAllAsync();
            return users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider);
        }
    }
}
