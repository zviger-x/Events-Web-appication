using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserCreateUseCase : BaseUseCase, ICreateUseCase<EventUser>
    {
        public EventUserCreateUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Create(EventUser entity)
        {
            await _unitOfWork.EventUserRepository.Create(entity);
        }
    }
}
