using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.WebAPI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly ICreateUseCase<EventDTO> _eventCreateUseCase;
        private readonly IDeleteUseCase<EventDTO> _eventDeleteUseCase;
        private readonly IUpdateUseCase<EventDTO> _eventUpdateUseCase;
        private readonly IGetByIdUseCase<EventDTO> _eventGetByIdUseCase;
        private readonly IGetAllUseCase<EventDTO> _eventGetAllUseCase;
        private readonly IGetPaginatedListUseCase<EventDTO> _eventGetPaginatedListUseCase;
        private readonly IGetEventByCategoryUseCase _eventGetByCategoryUseCase;
        private readonly IGetEventByDateUseCase _eventGetByDateUseCase;
        private readonly IGetEventByNameUseCase _eventGetByNameUseCase;
        private readonly IGetEventByVenueUseCase _eventGetByVenueUseCase;

        public EventsController(ICreateUseCase<EventDTO> eventCreateUseCase,
            IDeleteUseCase<EventDTO> eventDeleteUseCase,
            IUpdateUseCase<EventDTO> eventUpdateUseCase,
            IGetByIdUseCase<EventDTO> eventGetByIdUseCase,
            IGetAllUseCase<EventDTO> eventGetAllUseCase,
            IGetPaginatedListUseCase<EventDTO> eventGetPaginatedListUseCase,
            IGetEventByCategoryUseCase eventGetByCategoryUseCase,
            IGetEventByDateUseCase eventGetByDateUseCase,
            IGetEventByNameUseCase eventGetByNameUseCase,
            IGetEventByVenueUseCase eventGetByVenueUseCase)
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
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDTO>>> GetAll()
        {
            var events = await _eventGetAllUseCase.GetAll().ToListAsync();
            return Ok(events);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
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
        // [ValidateAntiForgeryToken]
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
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventDeleteUseCase.DeleteAsync(new EventDTO() { Id = id });

            return Ok();
        }
    }
}
