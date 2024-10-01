using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.DataAccess.Repositories
{
    internal class EventUserRepository : BaseRepository<EventUser>, IEventUserRepository
    {
        public EventUserRepository(EventsManagementDbContext context) : base(context)
        {
        }

        public override async Task<EventUser> GetByIdAsync(int id)
        {
            return await Context.FindAsync<EventUser>(id);
        }

        public override IQueryable<EventUser> GetAll()
        {
            return Context.EventUsers;
        }

        public async Task RegisterUserInEventAsync(EventUser eventUser)
        {
            await CreateAsync(eventUser);
        }

        public void UnregisterUserInEvent(EventUser eventUser)
        {
            Delete(eventUser);
        }

        public IQueryable<EventUser> GetUsersOfEvent(int eventId)
        {
            return Context.EventUsers.Where(a => a.EventId == eventId);
        }
    }
}
