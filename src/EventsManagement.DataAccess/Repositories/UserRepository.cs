using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataObjects.Entities;
using EventsManagement.DataObjects.Utilities.Interfaces;
using EventsManagement.DataObjects.Utilities;

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

        public override async Task<IPaginatedList<User>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            return await PaginatedList<User>.CreateAsync(Context.Users, pageIndex, pageSize);
        }
    }
}
