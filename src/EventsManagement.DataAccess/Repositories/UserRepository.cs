using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities;
using EventsManagement.DataObjects.Utilities;
using EventsManagement.DataObjects.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.DataAccess.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
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

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
