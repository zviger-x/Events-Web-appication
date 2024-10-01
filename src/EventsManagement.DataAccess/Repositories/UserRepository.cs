using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.DataAccess.Repositories
{
    internal class UserRepository : BaseRepository<User>
    {
        public UserRepository(EventsManagementDbContext context) : base(context)
        {
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await Context.FindAsync<User>(id);
        }

        public override IQueryable<User> GetAll()
        {
            return Context.Users;
        }
    }
}
