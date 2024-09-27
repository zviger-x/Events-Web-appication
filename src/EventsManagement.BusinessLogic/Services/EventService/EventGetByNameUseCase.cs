using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByNameUseCase : BaseUseCase, IGetEventByNameUseCase
    {
        public EventGetByNameUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<EventDTO> GetByName(string name)
        {
            var e = await _unitOfWork.EventRepository.GetByMame(name);
            return _mapper.Map<EventDTO>(e);
        }
    }
}
