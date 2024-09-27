using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserUpdateUseCase : BaseUseCase, IUpdateUseCase<EventUser>
    {
        public EventUserUpdateUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Update(EventUser entity)
        {
            await _unitOfWork.EventUserRepository.Update(entity);
        }
    }
}
