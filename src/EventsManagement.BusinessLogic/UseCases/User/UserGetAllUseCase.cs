using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.UseCases.UserUseCases
{
    internal class UserGetAllUseCase : BaseUseCase<UserDTO>, IGetAllUseCase<UserDTO>
    {
        public UserGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<UserDTO>> Execute()
        {
            var users = await _unitOfWork.UserRepository.GetAll().ToListAsync();
            var usersDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return usersDTOs;
        }
    }
}
