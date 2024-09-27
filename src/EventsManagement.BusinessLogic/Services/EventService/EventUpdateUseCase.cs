using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventUpdateUseCase : BaseUseCase, IUpdateUseCase<Event>
    {
        public EventUpdateUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Update(Event entity)
        {
            await _unitOfWork.EventRepository.Update(entity);
        }
    }
}
