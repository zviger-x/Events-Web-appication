using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetByIdUseCase : BaseUseCase, IGetByIdUseCase<EventUserDTO>
    {
        public EventUserGetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<EventUserDTO> GetById(int id)
        {
            var e = await _unitOfWork.EventUserRepository.GetById(id);
            return _mapper.Map<EventUserDTO>(e);
        }
    }
}
