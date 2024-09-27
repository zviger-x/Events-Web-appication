using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.DataAccess.Repositories
{
    internal class EventUserRepository : BaseRepository<EventUser>
    {
        public EventUserRepository(EventsManagementDbContext context) : base(context)
        {
        }

        public override async Task<EventUser> GetById(int id)
        {
            return await Context.FindAsync<EventUser>(id);
        }

        public override IQueryable<EventUser> GetAll()
        {
            return Context.EventUsers;
        }
    }
}
