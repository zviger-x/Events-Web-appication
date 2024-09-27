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
        public virtual async Task Create(T entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public virtual async Task Update(T entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public virtual async Task Delete(T entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public abstract Task<T> GetById(int id);

        /// <inheritdoc/>
        public abstract IQueryable<T> GetAll();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
