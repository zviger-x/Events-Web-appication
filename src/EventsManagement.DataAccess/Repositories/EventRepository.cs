using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities;
using EventsManagement.DataObjects.Utilities;
using EventsManagement.DataObjects.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(EventsManagementDbContext context) : base(context)
        {
        }

        public override async Task<Event> GetByIdAsync(int id)
        {
            return await Context.FindAsync<Event>(id);
        }

        public override IQueryable<Event> GetAll()
        {
            return Context.Events;
        }

        public override async Task<IPaginatedList<Event>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            return await PaginatedList<Event>.CreateAsync(Context.Events, pageIndex, pageSize);
        }

        public async Task<Event> GetByNameAsync(string name)
        {
            return await Context.Events.FirstOrDefaultAsync(e => e.Name == name);
        }

        public IQueryable<Event> GetByDate(DateTime date)
        {
            return Context.Events.Where(e => e.DateAndTime.Date == date.Date);
        }

        public IQueryable<Event> GetByVenue(string venue)
        {
            return Context.Events.Where(e => e.Venue.Contains(venue));
        }

        public IQueryable<Event> GetByCategory(string category)
        {
            return Context.Events.Where(e => e.Category.Contains(category));
        }
    }
}
