using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<Event>
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
    }
}
