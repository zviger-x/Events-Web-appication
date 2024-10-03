using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.DataTransferObjects
{
    public class EventUserDTO : IEntityDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
