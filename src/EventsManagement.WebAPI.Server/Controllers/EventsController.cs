using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventsManagement.WebAPI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private const int PageSize = 4;

        private readonly ICreateUseCase<EventDTO> _eventCreateUseCase;
        private readonly IDeleteUseCase<EventDTO> _eventDeleteUseCase;
        private readonly IUpdateUseCase<EventDTO> _eventUpdateUseCase;
        private readonly IGetByIdUseCase<EventDTO> _eventGetByIdUseCase;
        private readonly IGetAllUseCase<EventDTO> _eventGetAllUseCase;
        private readonly IGetPaginatedListUseCase<EventDTO> _eventGetPaginatedListUseCase;
        private readonly IGetEventsByCategoryUseCase _eventGetByCategoryUseCase;
        private readonly IGetEventsByDateUseCase _eventGetByDateUseCase;
        private readonly IGetEventByNameUseCase _eventGetByNameUseCase;
        private readonly IGetEventsByVenueUseCase _eventGetByVenueUseCase;
        private readonly IGetUsersOfEventUseCase _getUsersOfEventUseCase;

        public EventsController(ICreateUseCase<EventDTO> eventCreateUseCase,
            IDeleteUseCase<EventDTO> eventDeleteUseCase,
            IUpdateUseCase<EventDTO> eventUpdateUseCase,
            IGetByIdUseCase<EventDTO> eventGetByIdUseCase,
            IGetAllUseCase<EventDTO> eventGetAllUseCase,
            IGetPaginatedListUseCase<EventDTO> eventGetPaginatedListUseCase,
            IGetEventsByCategoryUseCase eventGetByCategoryUseCase,
            IGetEventsByDateUseCase eventGetByDateUseCase,
            IGetEventByNameUseCase eventGetByNameUseCase,
            IGetEventsByVenueUseCase eventGetByVenueUseCase,
            IGetUsersOfEventUseCase getUsersOfEventUseCase)
        {
            _eventCreateUseCase = eventCreateUseCase;
            _eventDeleteUseCase = eventDeleteUseCase;
            _eventUpdateUseCase = eventUpdateUseCase;
            _eventGetByIdUseCase = eventGetByIdUseCase;
            _eventGetAllUseCase = eventGetAllUseCase;
            _eventGetPaginatedListUseCase = eventGetPaginatedListUseCase;
            _eventGetByCategoryUseCase = eventGetByCategoryUseCase;
            _eventGetByDateUseCase = eventGetByDateUseCase;
            _eventGetByNameUseCase = eventGetByNameUseCase;
            _eventGetByVenueUseCase = eventGetByVenueUseCase;
            _getUsersOfEventUseCase = getUsersOfEventUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDTO>>> GetAll(
            [FromQuery] string? sortby = null,
            [FromQuery] string? value = null,
            [FromQuery] string? page = null)
        {
            if (string.IsNullOrEmpty(sortby) || string.IsNullOrEmpty(value))
            {
                if (!int.TryParse(page, out int pn))
                    return Ok(await _eventGetAllUseCase.GetAllAsync());

                return (await _eventGetPaginatedListUseCase.GetPaginatedListAsync(pn, PageSize)).Items;
            }

            IEnumerable<EventDTO> events;
            switch (sortby)
            {
                case SortValues.Name:
                    var evt = await _eventGetByNameUseCase.GetByNameAsync(value);
                    return Ok(new List<EventDTO> { evt });
                case SortValues.Category:
                    events = await _eventGetByCategoryUseCase.GetByCategoryAsync(value);
                    break;
                case SortValues.Venue:
                    events = await _eventGetByVenueUseCase.GetByVenueAsync(value);
                    break;
                case SortValues.Date:
                    if (!DateTime.TryParse(value, out DateTime date))
                        return Ok(await _eventGetAllUseCase.GetAllAsync());
                    events = await _eventGetByDateUseCase.GetByDateAsync(date);
                    break;
                default:
                    return Ok(await _eventGetAllUseCase.GetAllAsync());
            }

            if (!int.TryParse(page, out int pageNum))
                return Ok(events);

            var paginatedEvents = await _eventGetPaginatedListUseCase.GetPaginatedListAsync(events, pageNum, PageSize);
            return Ok(paginatedEvents.Items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetById(int id)
        {
            var @event = await _eventGetByIdUseCase.GetByIdAsync(id);

            return Ok(@event);
        }

        [HttpPost]
        public async Task<ActionResult<EventDTO>> Create([FromBody] EventDTO @event)
        {
            try
            {
                await _eventCreateUseCase.CreateAsync(@event);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(@event);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventDTO>> Edit(int id, [FromBody] EventDTO @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            try
            {
                await _eventUpdateUseCase.UpdateAsync(@event);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(@event);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventDeleteUseCase.DeleteAsync(new EventDTO() { Id = id });

            return Ok();
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
