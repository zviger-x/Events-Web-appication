namespace EventsManagement.BusinessLogic.Validation.Messages
{
    internal static class EventUserValidationMessages
    {
        public const string UserIdNotNull = "User ID cannot be null.";
        public const string UserIdInvalid = "User ID must be greater than 0.";
        public const string UserNotFound = "User does not exist.";

        public const string EventIdNotNull = "Event ID cannot be null.";
        public const string EventIdInvalid = "Event ID must be greater than 0.";
        public const string EventNotFound = "Event does not exist.";
        public const string EventMaxParticipantsReached = "The event has reached the maximum number of participants.";

        public const string RegistrationDateNotNull = "Registration date cannot be null.";
        public const string RegistrationDateInTheFuture = "Registration date cannot be in the future.";
    }
}
