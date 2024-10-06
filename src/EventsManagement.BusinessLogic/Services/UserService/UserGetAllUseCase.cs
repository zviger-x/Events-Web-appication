using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserGetAllUseCase : BaseUseCase<UserDTO>, IGetAllUseCase<UserDTO>
    {
        public UserGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAll().ToListAsync();
            var usersDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return usersDTOs;
        }
    }
}
