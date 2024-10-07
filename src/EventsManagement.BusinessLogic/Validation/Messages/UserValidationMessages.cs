namespace EventsManagement.BusinessLogic.Validation.Messages
{
    internal static class UserValidationMessages
    {
        public const string NameNotNull = "User name cannot be null.";
        public const string NameNotEmpty = "User name cannot be empty.";

        public const string SurnameNotNull = "User surname cannot be null.";
        public const string SurnameNotEmpty = "User surname cannot be empty.";

        public const string BirthDateNotNull = "Birth date cannot be null.";
        public const string BirthDateInTheFuture = "Birth date cannot be in the future.";

        public const string EmailNotNull = "Email cannot be null.";
        public const string EmailNotEmpty = "Email cannot be empty.";
        public const string EmailInvalid = "Email format is invalid.";
        public const string EmailMustBeUnique = "Email must be unique.";
    }
}
