using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetAllUseCase : BaseUseCase, IGetAllUseCase<Event>
    {
        public EventGetAllUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Event> GetAll()
        {
            return _unitOfWork.EventRepository.GetAll();
        }
    }
}
