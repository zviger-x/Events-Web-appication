using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.Event;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Utilities;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.UseCases.EventUseCases
{
    internal class EventGetAllSortedAndPaginatedUseCase : BaseUseCase<EventDTO>, IGetEventsSortedAndPaginatedUseCase
    {
        public EventGetAllSortedAndPaginatedUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<EventDTO>> Execute((string? sortBy, string? value, string? pageNumber, int pageSize) request)
        {
            if (string.IsNullOrEmpty(request.sortBy) || string.IsNullOrEmpty(request.value))
            {
                var e = await _unitOfWork.EventRepository.GetAll().ToListAsync();
                var edtos = _mapper.Map<IEnumerable<EventDTO>>(e);
                if (!int.TryParse(request.pageNumber, out int pn) || request.pageSize <= 1)
                    return edtos;

                var pl = await PaginatedList<EventDTO>.CreateAsync(edtos, pn, request.pageSize);
                return pl.Items;
            }
            else
            {
                IEnumerable<EventDTO> events;
                switch (request.sortBy)
                {
                    case SortValues.Name:
                        var e = await _unitOfWork.EventRepository.GetByNameAsync(request.value);
                        var el = new List<EventDTO>();
                        if (e != null) el.Add(_mapper.Map<EventDTO>(e));
                        return el;

                    case SortValues.Category:
                        var ec = await _unitOfWork.EventRepository.GetByCategory(request.value).ToListAsync();
                        events = _mapper.Map<IEnumerable<EventDTO>>(ec);
                        break;

                    case SortValues.Venue:
                        var ev = await _unitOfWork.EventRepository.GetByVenue(request.value).ToListAsync();
                        events = _mapper.Map<IEnumerable<EventDTO>>(ev);
                        break;

                    case SortValues.Date:
                        if (!DateTime.TryParse(request.value, out DateTime date))
                            return new List<EventDTO>();
                        var ed = await _unitOfWork.EventRepository.GetByDate(date).ToListAsync();
                        events = _mapper.Map<IEnumerable<EventDTO>>(ed);
                        break;

                    default:
                        var ea = await _unitOfWork.EventRepository.GetAll().ToListAsync();
                        var edtos = _mapper.Map<IEnumerable<EventDTO>>(ea);
                        return edtos;
                }

                if (!int.TryParse(request.pageNumber, out int pageNum) || request.pageSize <= 1)
                    return events;

                var paginatedEvents = await PaginatedList<EventDTO>.CreateAsync(events, pageNum, request.pageSize);
                return paginatedEvents.Items;
            }
        }

        private static class SortValues
        {
            public const string Name = "name";
            public const string Category = "category";
            public const string Venue = "venue";
            public const string Date = "date";
        }
    }
}
