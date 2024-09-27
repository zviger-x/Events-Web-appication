using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserGetByIdUseCase : BaseUseCase, IGetByIdUseCase<UserDTO>
    {
        public UserGetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<UserDTO> GetById(int id)
        {
            var u = await _unitOfWork.UserRepository.GetById(id);
            return _mapper.Map<UserDTO>(u);
        }
    }
}
