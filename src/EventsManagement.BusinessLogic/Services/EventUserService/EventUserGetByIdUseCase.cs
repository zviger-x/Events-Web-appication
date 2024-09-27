using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetByIdUseCase : BaseUseCase, IGetByIdUseCase<EventUser>
    {
        public EventUserGetByIdUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<EventUser> GetById(int id)
        {
            return await _unitOfWork.EventUserRepository.GetById(id);
        }
    }
}
