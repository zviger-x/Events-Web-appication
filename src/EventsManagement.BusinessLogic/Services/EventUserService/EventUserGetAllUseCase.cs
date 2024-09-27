using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetAllUseCase : BaseUseCase, IGetAllUseCase<EventUser>
    {
        public EventUserGetAllUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<EventUser> GetAll()
        {
            return _unitOfWork.EventUserRepository.GetAll();
        }
    }
}
