using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventUpdateUseCase : BaseUseCase, IUpdateUseCase<EventDTO>
    {
        public EventUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task Update(EventDTO entity)
        {
            var e = _mapper.Map<Event>(entity);
            await _unitOfWork.EventRepository.Update(e);
        }
    }
}
