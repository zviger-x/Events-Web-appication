using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Utilities;
using EventsManagement.DataObjects.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var events = await _unitOfWork.EventRepository.GetAll().ToListAsync();
            var eventDTOs = _mapper.Map<IEnumerable<EventDTO>>(events);
            return await PaginatedList<EventDTO>.CreateAsync(eventDTOs, pageIndex, pageSize);
        }

        public async Task<IPaginatedList<EventDTO>> GetPaginatedListAsync(IEnumerable<EventDTO> entities, int pageIndex, int pageSize)
        {
            return await PaginatedList<EventDTO>.CreateAsync(entities, pageIndex, pageSize);
        }
    }
}
