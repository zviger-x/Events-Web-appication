using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventCreateUseCase : BaseUseCase, ICreateUseCase<EventDTO>
    {
        public EventCreateUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task Create(EventDTO entity)
        {
            var e = _mapper.Map<Event>(entity);
            await _unitOfWork.EventRepository.Create(e);
        }
    }
}
