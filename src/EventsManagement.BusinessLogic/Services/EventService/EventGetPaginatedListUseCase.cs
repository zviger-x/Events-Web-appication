using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataObjects.Utilities;
using EventsManagement.DataObjects.Utilities.Interfaces;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetPaginatedListUseCase : BaseUseCase<EventDTO>, IGetPaginatedListUseCase<EventDTO>
    {
        public EventGetPaginatedListUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IPaginatedList<EventDTO>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            var events = _unitOfWork.EventRepository.GetAll();
            var eventDTOs = events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
            return await PaginatedList<EventDTO>.CreateAsync(eventDTOs, pageIndex, pageSize);
        }
    }
}
