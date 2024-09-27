using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserCreateUseCase : BaseUseCase, ICreateUseCase<UserDTO>
    {
        public UserCreateUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task Create(UserDTO entity)
        {
            var u = _mapper.Map<User>(entity);
            await _unitOfWork.UserRepository.Create(u);
        }
    }
}
