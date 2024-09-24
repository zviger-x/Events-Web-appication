using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.DataObjects.Entities
{
    internal class EventCategory : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
