using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.DataAccess.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T>, IDisposable
        where T : IEntity
    {
        private bool _disposed;

        protected BaseRepository(EventsManagementDbContext context)
        {
            Context = context;
        }

        protected EventsManagementDbContext Context { get; }

        /// <inheritdoc/>
        public virtual async Task CreateAsync(T entity)
        {
            await Context.AddAsync(entity);
        }

        /// <inheritdoc/>
        public virtual void Update(T entity)
        {
            Context.Update(entity);
        }

        /// <inheritdoc/>
        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
        }

        /// <inheritdoc/>
        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public abstract Task<T> GetByIdAsync(int id);

        /// <inheritdoc/>
        public abstract IQueryable<T> GetAllAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
