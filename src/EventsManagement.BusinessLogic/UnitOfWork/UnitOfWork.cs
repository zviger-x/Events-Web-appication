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

        private IEventRepository _eventRepository;
        private IUserRepository _userRepository;
        private IEventUserRepository _eventUserRepository;

        public UnitOfWork(EventsManagementDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IEventRepository EventRepository => _eventRepository ??= _serviceProvider.GetRequiredService<IEventRepository>();

        public IUserRepository UserRepository => _userRepository ??= _serviceProvider.GetRequiredService<IUserRepository>();

        public IEventUserRepository EventUserRepository => _eventUserRepository ??= _serviceProvider.GetRequiredService<IEventUserRepository>();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
