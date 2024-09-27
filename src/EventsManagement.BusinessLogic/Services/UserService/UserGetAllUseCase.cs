using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserGetAllUseCase : BaseUseCase, IGetAllUseCase<UserDTO>
    {
        public UserGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IQueryable<UserDTO> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider);
        }
    }
}
