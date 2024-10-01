using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataObjects.Utilities.Interfaces;
using EventsManagement.DataObjects.Utilities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetPaginatedListUseCase : BaseUseCase<EventUserDTO>, IGetPaginatedListUseCase<EventUserDTO>
    {
        public EventUserGetPaginatedListUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IPaginatedList<EventUserDTO>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            var eventUsers = _unitOfWork.EventRepository.GetAll();
            var eventUsersDTOs = eventUsers.ProjectTo<EventUserDTO>(_mapper.ConfigurationProvider);
            return await PaginatedList<EventUserDTO>.CreateAsync(eventUsersDTOs, pageIndex, pageSize);
        }
    }
}
