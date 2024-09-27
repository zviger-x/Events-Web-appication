using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventDeleteUseCase : BaseUseCase, IDeleteUseCase<Event>
    {
        public EventDeleteUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Delete(Event entity)
        {
            await _unitOfWork.EventRepository.Delete(entity);
        }
    }
}
