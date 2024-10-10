using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UseCases.Interfaces.Event;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IGetEventsSortedAndPaginatedUseCase _getEventsSortedAndPaginatedUseCase;
        private readonly IGetUsersOfEventUseCase _getUsersOfEventUseCase;

        public EventsController(ICreateUseCase<EventDTO> eventCreateUseCase,
            IDeleteUseCase<EventDTO> eventDeleteUseCase,
            IUpdateUseCase<EventDTO> eventUpdateUseCase,
            IGetByIdUseCase<EventDTO> eventGetByIdUseCase,
            IGetAllUseCase<EventDTO> eventGetAllUseCase,
            IGetEventsSortedAndPaginatedUseCase getEventsSortedAndPaginatedUseCase,
            IGetUsersOfEventUseCase getUsersOfEventUseCase)
        {
            _eventCreateUseCase = eventCreateUseCase;
            _eventDeleteUseCase = eventDeleteUseCase;
            _eventUpdateUseCase = eventUpdateUseCase;
            _eventGetByIdUseCase = eventGetByIdUseCase;
            _eventGetAllUseCase = eventGetAllUseCase;
            _getEventsSortedAndPaginatedUseCase = getEventsSortedAndPaginatedUseCase;
            _getUsersOfEventUseCase = getUsersOfEventUseCase;
        }

        [HttpGet("error")]
        public IActionResult GetError()
        {
            throw new InvalidOperationException("This is a test exception.");
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDTO>>> GetAll(
            [FromQuery] string? sortby = null,
            [FromQuery] string? value = null,
            [FromQuery] string? page = null)
        {
            Console.WriteLine($"[Controller] {sortby}, {value}, {page}, {PageSize}");

            try
            {
                var events = await _getEventsSortedAndPaginatedUseCase.Execute((sortby, value, page, PageSize));
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetById(int id)
        {
            var @event = await _eventGetByIdUseCase.Execute(id);

            return Ok(@event);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EventDTO>> Create([FromBody] EventDTO @event)
        {
            try
            {
                await _eventCreateUseCase.Execute(@event);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(@event);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EventDTO>> Edit(int id, [FromBody] EventDTO @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            try
            {
                await _eventUpdateUseCase.Execute(@event);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(@event);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventDeleteUseCase.Execute(new EventDTO() { Id = id });

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
