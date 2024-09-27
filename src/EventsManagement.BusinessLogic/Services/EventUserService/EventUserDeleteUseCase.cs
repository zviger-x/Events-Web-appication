using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserDeleteUseCase : BaseUseCase, IDeleteUseCase<EventUser>
    {
        public EventUserDeleteUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Delete(EventUser entity)
        {
            await _unitOfWork.EventUserRepository.Delete(entity);
        }
    }
}
