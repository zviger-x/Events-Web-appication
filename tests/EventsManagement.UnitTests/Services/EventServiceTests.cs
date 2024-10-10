using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.EventService;
using EventsManagement.BusinessLogic.UseCases.Event;
using EventsManagement.BusinessLogic.Validation.Validators;
using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MockQueryable;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace EventsManagement.UnitTests.Services
{
    [TestFixture]
    internal class EventServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IEventRepository> _eventRepositoryMock;
        private EventValidator _validator;

        private EventCreateUseCase _eventCreateUseCase;
        private EventUpdateUseCase _eventUpdateUseCase;
        private EventDeleteUseCase _eventDeleteUseCase;
        private EventGetAllUseCase _eventGetAllUseCase;
        private EventGetAllSortedAndPaginatedUseCase _eventGetAllSortedAndPaginatedUseCase;
        private EventGetByIdUseCase _eventGetByIdUseCase;

        [SetUp]
        public void Setup()
        {
            var eventsMock = new List<Event>().AsQueryable().BuildMock();
            _eventRepositoryMock = new Mock<IEventRepository>();
            _eventRepositoryMock.Setup(repo => repo.GetAll()).Returns(eventsMock);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.EventRepository).Returns(_eventRepositoryMock.Object);
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDTO>();
                cfg.CreateMap<EventDTO, Event>();
            }));

            _validator = new EventValidator(_unitOfWorkMock.Object);
            _eventCreateUseCase = new EventCreateUseCase(_unitOfWorkMock.Object, _mapperMock.Object, _validator);
            _eventUpdateUseCase = new EventUpdateUseCase(_unitOfWorkMock.Object, _mapperMock.Object, _validator);
            _eventDeleteUseCase = new EventDeleteUseCase(_unitOfWorkMock.Object, _mapperMock.Object, _validator);
            _eventGetAllUseCase = new EventGetAllUseCase(_unitOfWorkMock.Object, _mapperMock.Object, _validator);
            _eventGetAllSortedAndPaginatedUseCase = new EventGetAllSortedAndPaginatedUseCase(_unitOfWorkMock.Object, _mapperMock.Object, _validator);
            _eventGetByIdUseCase = new EventGetByIdUseCase(_unitOfWorkMock.Object, _mapperMock.Object, _validator);
        }

        #region --- Positive Cases ---
        [Test]
        public async Task EventCreateUseCase_ShouldCreateEvent()
        {
            // Arrange
            var eventDTO = new EventDTO
            {
                Id = 1,
                Name = "New Event",
                Description = "Description",
                Category = "Category",
                DateAndTime = DateTime.Now,
                MaxNumberOfParticipants = 1,
                Venue = "Venue"
            };
            var eventEntity = _mapperMock.Object.Map<Event>(eventDTO);

            _mapperMock.Setup(m => m.Map<Event>(eventDTO)).Returns(eventEntity);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.CreateAsync(eventEntity)).Returns(Task.CompletedTask);

            // Act
            await _eventCreateUseCase.Execute(eventDTO);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.EventRepository.CreateAsync(eventEntity), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.EventRepository.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task EventUpdateUseCase_ShouldUpdateEvent()
        {
            // Arrange
            var eventDTO = new EventDTO
            {
                Id = 1,
                Name = "New Event",
                Description = "Description",
                Category = "Category",
                DateAndTime = DateTime.Now,
                MaxNumberOfParticipants = 1,
                Venue = "Venue"
            };
            var eventEntity = _mapperMock.Object.Map<Event>(eventDTO);

            _mapperMock.Setup(m => m.Map<Event>(eventDTO)).Returns(eventEntity);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.Update(eventEntity)).Verifiable();

            // Act
            await _eventUpdateUseCase.Execute(eventDTO);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.EventRepository.Update(eventEntity), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.EventRepository.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task EventDeleteUseCase_ShouldDeleteEvent()
        {
            // Arrange
            var eventDTO = new EventDTO
            {
                Id = 1,
                Name = "New Event",
                Description = "Description",
                Category = "Category",
                DateAndTime = DateTime.Now,
                MaxNumberOfParticipants = 1,
                Venue = "Venue"
            };
            var eventEntity = _mapperMock.Object.Map<Event>(eventDTO);

            _mapperMock.Setup(m => m.Map<Event>(eventDTO)).Returns(eventEntity);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.Delete(eventEntity)).Verifiable();

            // Act
            await _eventDeleteUseCase.Execute(eventDTO);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.EventRepository.Delete(eventEntity), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.EventRepository.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task EventGetAllUseCase_ShouldReturnAllEvents()
        {
            // Arrange
            var eventEntities = new List<Event>
            {
                new Event { Id = 1, Name = "Event 1" },
                new Event { Id = 2, Name = "Event 2" }
            }.AsQueryable().BuildMock();

            var eventDTOs = new List<EventDTO>
            {
                new EventDTO { Id = 1, Name = "Event 1" },
                new EventDTO { Id = 2, Name = "Event 2" }
            }.AsQueryable().BuildMock();

            _mapperMock.Setup(m => m.Map<IEnumerable<EventDTO>>(It.IsAny<IEnumerable<Event>>())).Returns(eventDTOs);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetAll()).Returns(eventEntities);

            // Act
            var result = await _eventGetAllUseCase.Execute();

            // Assert
            _unitOfWorkMock.Verify(uow => uow.EventRepository.GetAll(), Times.Once);
            ClassicAssert.AreEqual(eventDTOs.Count(), result.Count());
        }
        
        [Test]
        public async Task GetPaginatedListAsync_ShouldReturnPaginatedList()
        {
            // Arrange
            var pageNumber = 2;
            var pageSize = 2;
            var eventEntities = new List<Event>
            {
                new Event { Id = 1, Name = "Event 1" },
                new Event { Id = 2, Name = "Event 2" },
                new Event { Id = 3, Name = "Event 3" },  // Should
                new Event { Id = 4, Name = "Event 4" }   // return
            }.AsQueryable().BuildMock();
            var mappedEvents = new List<EventDTO>
            {
                new EventDTO { Id = 1, Name = "Event 1" },
                new EventDTO { Id = 2, Name = "Event 2" },
                new EventDTO { Id = 3, Name = "Event 3" },
                new EventDTO { Id = 4, Name = "Event 4" }
            };

            var excpectedEventDTOs = new List<EventDTO>
            {
                new EventDTO { Id = 3, Name = "Event 3" },
                new EventDTO { Id = 4, Name = "Event 4" }
            };

            _mapperMock.Setup(m => m.Map<IEnumerable<EventDTO>>(It.IsAny<IEnumerable<Event>>())).Returns(mappedEvents);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetAll()).Returns(eventEntities);

            // Act
            var result = await _eventGetAllSortedAndPaginatedUseCase.Execute((null, null, pageNumber.ToString(), pageSize));

            // Assert
            _unitOfWorkMock.Verify(uow => uow.EventRepository.GetAll(), Times.Once);
            ClassicAssert.IsTrue(excpectedEventDTOs.First().Id == result.First().Id);
        }

        [Test]
        public async Task GetSortedListAsync_SortByName_ShouldReturnPaginatedList()
        {
            // Arrange
            string sortBy = "name";
            string value = "Event 2";
            var eventEntities = new List<Event>
            {
                new Event { Id = 1, Name = "Event 1", Category = "Category 1", Venue = "Venue 1" },
                new Event { Id = 2, Name = "Event 2", Category = "Category 2", Venue = "Venue 2" },
                new Event { Id = 3, Name = "Event 3", Category = "Category 3", Venue = "Venue 3" },
                new Event { Id = 4, Name = "Event 4", Category = "Category 4", Venue = "Venue 4" }
            }.AsQueryable().BuildMock();
            var expectedEventDTOs = new List<EventDTO>
            {
                new EventDTO { Id = 2, Name = "Event 2", Category = "Category 2", Venue = "Venue 2" }
            };

            _mapperMock.Setup(m => m.Map<EventDTO>(eventEntities.ElementAt(1))).Returns(expectedEventDTOs.First());
            _mapperMock.Setup(m => m.Map<IEnumerable<EventDTO>>(It.IsAny<IEnumerable<Event>>())).Returns(expectedEventDTOs);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetAll()).Returns(eventEntities);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetByNameAsync(value)).ReturnsAsync(eventEntities.ElementAt(1));

            // Act
            var result = await _eventGetAllSortedAndPaginatedUseCase.Execute((sortBy, value, null, default));

            // Assert
            ClassicAssert.AreEqual(expectedEventDTOs.First().Id, result.First().Id);
        }

        [Test]
        public async Task GetSortedListAsync_SortByCategory_ShouldReturnPaginatedList()
        {
            // Arrange
            string sortBy = "category";
            string value = "Category 2";
            var eventEntities = new List<Event>
            {
                new Event { Id = 1, Name = "Event 1", Category = "Category 1", Venue = "Venue 1" },
                new Event { Id = 2, Name = "Event 2", Category = "Category 2", Venue = "Venue 2" },
                new Event { Id = 3, Name = "Event 3", Category = "Category 3", Venue = "Venue 3" },
                new Event { Id = 4, Name = "Event 4", Category = "Category 4", Venue = "Venue 4" }
            }.AsQueryable().BuildMock();
            var expectedEventDTOs = new List<EventDTO>
            {
                new EventDTO { Id = 2, Name = "Event 2", Category = "Category 2", Venue = "Venue 2" }
            };

            _mapperMock.Setup(m => m.Map<EventDTO>(eventEntities.ElementAt(1))).Returns(expectedEventDTOs.First());
            _mapperMock.Setup(m => m.Map<IEnumerable<EventDTO>>(It.IsAny<IEnumerable<Event>>())).Returns(expectedEventDTOs);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetAll()).Returns(eventEntities);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetByCategory(value)).Returns(eventEntities);

            // Act
            var result = await _eventGetAllSortedAndPaginatedUseCase.Execute((sortBy, value, null, default));

            // Assert
            ClassicAssert.AreEqual(expectedEventDTOs.First().Id, result.First().Id);
        }

        [Test]
        public async Task GetSortedListAsync_SortByVenue_ShouldReturnPaginatedList()
        {
            // Arrange
            string sortBy = "venue";
            string value = "Venue 2";
            var eventEntities = new List<Event>
            {
                new Event { Id = 1, Name = "Event 1", Category = "Category 1", Venue = "Venue 1" },
                new Event { Id = 2, Name = "Event 2", Category = "Category 2", Venue = "Venue 2" },
                new Event { Id = 3, Name = "Event 3", Category = "Category 3", Venue = "Venue 3" },
                new Event { Id = 4, Name = "Event 4", Category = "Category 4", Venue = "Venue 4" }
            }.AsQueryable().BuildMock();
            var expectedEventDTOs = new List<EventDTO>
            {
                new EventDTO { Id = 2, Name = "Event 2", Category = "Category 2", Venue = "Venue 2" }
            };

            _mapperMock.Setup(m => m.Map<EventDTO>(eventEntities.ElementAt(1))).Returns(expectedEventDTOs.First());
            _mapperMock.Setup(m => m.Map<IEnumerable<EventDTO>>(It.IsAny<IEnumerable<Event>>())).Returns(expectedEventDTOs);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetAll()).Returns(eventEntities);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetByVenue(value)).Returns(eventEntities);

            // Act
            var result = await _eventGetAllSortedAndPaginatedUseCase.Execute((sortBy, value, null, default));

            // Assert
            ClassicAssert.AreEqual(expectedEventDTOs.First().Id, result.First().Id);
        }

        [Test]
        public async Task GetSortedListAsync_SortByDate_ShouldReturnPaginatedList()
        {
            // Arrange
            string sortBy = "date";
            string value = "10.10.2024";
            var eventEntities = new List<Event>
            {
                new Event { Id = 1, Name = "Event 1", Category = "Category 1", Venue = "Venue 1", DateAndTime = DateTime.Parse("09.10.2024"), },
                new Event { Id = 2, Name = "Event 2", Category = "Category 2", Venue = "Venue 2", DateAndTime = DateTime.Parse("10.10.2024"), },
                new Event { Id = 3, Name = "Event 3", Category = "Category 3", Venue = "Venue 3", DateAndTime = DateTime.Parse("11.10.2024"), },
                new Event { Id = 4, Name = "Event 4", Category = "Category 4", Venue = "Venue 4", DateAndTime = DateTime.Parse("12.10.2024") }
            }.AsQueryable().BuildMock();
            var expectedEventDTOs = new List<EventDTO>
            {
                new EventDTO { Id = 2, Name = "Event 2", Category = "Category 2", Venue = "Venue 2", DateAndTime = DateTime.Parse("10.10.2024") }
            };
            var parsedDate = DateTime.Parse(value);

            _mapperMock.Setup(m => m.Map<EventDTO>(eventEntities.ElementAt(1))).Returns(expectedEventDTOs.First());
            _mapperMock.Setup(m => m.Map<IEnumerable<EventDTO>>(It.IsAny<IEnumerable<Event>>())).Returns(expectedEventDTOs);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetAll()).Returns(eventEntities);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetByDate(parsedDate)).Returns(eventEntities);

            // Act
            var result = await _eventGetAllSortedAndPaginatedUseCase.Execute((sortBy, value, null, default));

            // Assert
            ClassicAssert.AreEqual(expectedEventDTOs.First().Id, result.First().Id);
        }

        [Test]
        public async Task EventGetByIdUseCase_ShouldReturnEventById()
        {
            // Arrange
            var eventId = 1;
            var eventEntity = new Event { Id = eventId, Name = "Event 1" };
            var eventDto = new EventDTO { Id = eventId, Name = "Event 2" };

            _mapperMock.Setup(m => m.Map<EventDTO>(eventEntity)).Returns(eventDto);
            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);

            // Act
            var result = await _eventGetByIdUseCase.Execute(eventId);

            // Assert
            _mapperMock.Verify(m => m.Map<EventDTO>(eventEntity), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.EventRepository.GetByIdAsync(eventId), Times.Once);
            ClassicAssert.AreEqual(eventDto, result);
        }
        #endregion

        #region --- Negative cases ---
        [TestCase(null, "Description", "Category", 10)]
        [TestCase("", "Description", "Category", 10)]
        [TestCase("Name", null, "Category", 10)]
        [TestCase("Name", "", "Category", 10)]
        [TestCase("Name", "Description", null, 10)]
        [TestCase("Name", "Description", "", 10)]
        [TestCase("Name", "Description", "Category", 0)]
        public void EventCreateUseCase_ShouldThrowValidationException(string name, string description, string category, int maxParticipants)
        {
            // Arrange
            var eventDTO = new EventDTO
            {
                Name = name,
                Description = description,
                Category = category,
                MaxNumberOfParticipants = maxParticipants
            };

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _eventCreateUseCase.Execute(eventDTO));
        }

        [TestCase(null, "Description", "Category", 10)]
        [TestCase("", "Description", "Category", 10)]
        [TestCase("Name", null, "Category", 10)]
        [TestCase("Name", "", "Category", 10)]
        [TestCase("Name", "Description", null, 10)]
        [TestCase("Name", "Description", "", 10)]
        [TestCase("Name", "Description", "Category", 0)]
        public void EventUpdateUseCase_ShouldThrowValidationException(string name, string description, string category, int maxParticipants)
        {
            // Arrange
            var eventDTO = new EventDTO
            {
                Name = name,
                Description = description,
                Category = category,
                MaxNumberOfParticipants = maxParticipants
            };

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _eventUpdateUseCase.Execute(eventDTO));
        }

        [TestCase(-1)]
        [TestCase(-512)]
        [TestCase(int.MinValue)]
        public void EventGetByIdUseCase_ShouldThrowArgumentOutOfRangeException(int id)
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _eventGetByIdUseCase.Execute(id));
        }

        [Test]
        public async Task GetPaginatedListAsync_ShouldReturnEmpty_WhenPageIndexIsTooHigh()
        {
            // Arrange
            var pageIndex = 10;
            var pageSize = 2;
            var eventEntities = new List<Event>().AsQueryable().BuildMock();

            _unitOfWorkMock.Setup(uow => uow.EventRepository.GetAll()).Returns(eventEntities);

            // Act
            var result = await _eventGetAllSortedAndPaginatedUseCase.Execute((null, null, pageIndex.ToString(), pageSize));

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(0, result.Count());
        }
        #endregion
    }
}