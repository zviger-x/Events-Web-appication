using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Utilities;
using EventsManagement.DataObjects.Utilities.Interfaces;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserGetPaginatedListUseCase : BaseUseCase<UserDTO>, IGetPaginatedListUseCase<UserDTO>
    {
        public UserGetPaginatedListUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IPaginatedList<UserDTO>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            var users = _unitOfWork.EventRepository.GetAll();
            var usersDTOs = users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider);
            return await PaginatedList<UserDTO>.CreateAsync(usersDTOs, pageIndex, pageSize);
        }

        public async Task<IPaginatedList<UserDTO>> GetPaginatedListAsync(IEnumerable<UserDTO> entities, int pageIndex, int pageSize)
        {
            return await PaginatedList<UserDTO>.CreateAsync(entities, pageIndex, pageSize);
        }
    }
}
