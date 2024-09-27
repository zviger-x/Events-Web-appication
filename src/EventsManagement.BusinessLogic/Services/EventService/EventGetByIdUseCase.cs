using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByIdUseCase : BaseUseCase, IGetByIdUseCase<Event>
    {
        public EventGetByIdUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<Event> GetById(int id)
        {
            return await _unitOfWork.EventRepository.GetById(id);
        }
    }
}
