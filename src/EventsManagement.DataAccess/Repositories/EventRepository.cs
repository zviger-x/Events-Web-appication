using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(EventsManagementDbContext context) : base(context)
        {
        }

        public override async Task<Event> GetById(int id)
        {
            return await Context.FindAsync<Event>(id);
        }

        public override IQueryable<Event> GetAll()
        {
            return Context.Events;
        }

        public async Task<Event> GetByMame(string name)
        {
            return await Context.Events.FirstOrDefaultAsync(e => e.Name == name);
        }

        public IQueryable<Event> GetByDate(DateTime date)
        {
            return Context.Events.Where(e => e.DateAndTime == date);
        }

        public IQueryable<Event> GetByVenue(string venue)
        {
            return Context.Events.Where(e => e.Venue == venue);
        }

        public IQueryable<Event> GetByCategory(string category)
        {
            return Context.Events.Where(e => e.Category == category);
        }
    }
}
