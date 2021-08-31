namespace Rookie.AssetManagement.Contracts.Constants
{
    public static class ErrorTypes
    {
        public static class Common
        {
            public const string FromDateLessThanToDateError = "From date must be less than To date.";
            public const string ToDateGreaterThanFromDateError = "To date must be greater than From date.";
            public const string FromNumberLessThanToNumberError = "From number must be less than To number.";
            public const string ToNumberGreaterThanFromNumberError = "To number must be greater than From number.";
            public const string MinLengthError = "The field must be at least {0} characters length.";
            public const string MaxLengthError = "The field must be maximum {0} characters length.";
            public const string PagingRequiredError = "Please specify skip/take parameters.";
            public const string InvalidSkipError = "Skip must be greater than or equal to {0}.";
            public const string InvalidTakeError = "Take must be between {0} and {1}.";
            public const string PropertyUnsortableError = "Property '{0}' is not sortable.";
            public const string RequiredError = "{0} must be provided.";
            public const string OutOfRangeError = "{0} must be between {1} and {2}.";
            public const string NumberGreaterThanError = "{0} must be greater than {1}.";
            public const string InvalidEnumError = "{0} is not a valid {1}";
        }

        public static class User
        {
            public const string DateOfBirthError = "{0} error : User must be 18 years old or older.";
            public const string JoinedDateError = "User can't join before their birthday";
            public const string JoinedDateWeekendError = "User can't join in the weekend";
            public const string UserTypeError = "User type must be ADMIN or STAFF";
            public const string UserNotFoundError = "User not found";
            public const string TokenNotFoundError = "Token not found";
            public const string JoinedDateWhenAgeNotEnough = "User under the age of 18 may not join company. Please select a different date";
            public const string NewPasswordIsNotDifferent = "New password must be different than old password";
        }

        public static class Assignment
        {
            public const string StateFilter = "{0} is not a in-progress state";
            public const string AssignmentNotFoundError = "Assignment not found";

        }
        
        public static class Asset
        {
            public const string AssetNotFoundError = "Asset not found";
        }
    }
}
