namespace EventsManagement.BusinessLogic.Validation.Messages
{
    internal static class EventValidationMessages
    {
        public const string NameNotNull = "Event name cannot be null.";
        public const string NameNotEmpty = "Event name cannot be empty.";
        public const string NameMustBeUnique = "Event name must be unique.";

        public const string DescriptionNotNull = "Description cannot be null.";
        public const string DescriptionNotEmpty = "Description cannot be empty.";

        public const string DateAndTimeNotNull = "Date and time cannot be null.";

        public const string CategoryNotNull = "Category cannot be null.";
        public const string CategoryNotEmpty = "Category cannot be empty.";

        public const string MaxParticipantsGreaterThanZero = "Max number of participants must be greater than 0.";
    }
}
