using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventDeleteUseCase : BaseUseCase, IDeleteUseCase<EventDTO>
    {
        public EventDeleteUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task Delete(EventDTO entity)
        {
            var e = _mapper.Map<Event>(entity);
            await _unitOfWork.EventRepository.Delete(e);
        }
    }
}
