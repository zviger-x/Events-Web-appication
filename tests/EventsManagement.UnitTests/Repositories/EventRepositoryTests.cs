using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataAccess.Repositories;
using EventsManagement.DataObjects.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace EventsManagement.UnitTests.Repositories
{
    [TestFixture]
    internal class EventRepositoryTests
    {
        private EventsManagementDbContext _context;
        private EventRepository _eventRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventsManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new EventsManagementDbContext(options);
            _eventRepository = new EventRepository(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectEvent()
        {
            // Arrange
            var eventEntity = new Event { Name = "Test Event", Description = "Description", Category = "Category", Venue = "Venue" };
            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _eventRepository.GetByIdAsync(1);

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(eventEntity.Id, result.Id);
            ClassicAssert.AreEqual(eventEntity.Name, result.Name);
            ClassicAssert.AreEqual(eventEntity.Description, result.Description);
            ClassicAssert.AreEqual(eventEntity.Category, result.Category);
            ClassicAssert.AreEqual(eventEntity.Venue, result.Venue);
        }

        [Test]
        public async Task SaveChangesAsync_ShouldSaveEventToDatabase()
        {
            // Arrange
            var eventName = Guid.NewGuid().ToString();
            var eventEntity = new Event { Name = eventName, Description = "D", Category = "C", Venue = "V" };
            _context.Events.Add(eventEntity);

            // Act
            await _eventRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Events.FindAsync(eventEntity.Id);
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(eventEntity.Id, result.Id);
            ClassicAssert.AreEqual(eventEntity.Name, result.Name);
            ClassicAssert.AreEqual(eventEntity.Description, result.Description);
        }
    }
}
