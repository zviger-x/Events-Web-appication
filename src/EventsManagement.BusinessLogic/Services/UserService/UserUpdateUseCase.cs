using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserUpdateUseCase : BaseUseCase, IUpdateUseCase<UserDTO>
    {
        public UserUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task Update(UserDTO entity)
        {
            var u = _mapper.Map<User>(entity);
            await _unitOfWork.UserRepository.Update(u);
        }
    }
}
