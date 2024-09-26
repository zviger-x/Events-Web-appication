using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.DataObjects.Entities
{
    internal class EventUser : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
