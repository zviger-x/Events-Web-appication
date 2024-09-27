using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByIdUseCase : BaseUseCase, IGetByIdUseCase<EventDTO>
    {
        public EventGetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<EventDTO> GetById(int id)
        {
            var e = await _unitOfWork.EventRepository.GetById(id);
            return _mapper.Map<EventDTO>(e);
        }
    }
}
