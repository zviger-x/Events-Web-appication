using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventCreateUseCase : BaseUseCase, ICreateUseCase<Event>
    {
        public EventCreateUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Create(Event entity)
        {
            await _unitOfWork.EventRepository.Create(entity);
        }
    }
}
