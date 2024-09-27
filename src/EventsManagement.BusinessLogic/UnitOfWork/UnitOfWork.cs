using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace EventsManagement.BusinessLogic.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EventsManagementDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        private IRepository<Event> _eventRepository;
        private IRepository<User> _userRepository;
        private IRepository<EventUser> _eventUserRepository;

        public UnitOfWork(EventsManagementDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IRepository<Event> EventRepository => _eventRepository ??= _serviceProvider.GetRequiredService<IRepository<Event>>();

        public IRepository<User> UserRepository => _userRepository ??= _serviceProvider.GetRequiredService<IRepository<User>>();

        public IRepository<EventUser> EventUserRepository => _eventUserRepository ??= _serviceProvider.GetRequiredService<IRepository<EventUser>>();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
