using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.DataObjects.Entities
{
    internal class Event : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateAndTime { get; set; }

        public int CategoryId { get; set; }

        public int MaxNumberOfParticipants { get; set; }

        public byte[] Image { get; set; }
    }
}
