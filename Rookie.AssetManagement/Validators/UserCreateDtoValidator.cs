using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;
using System;

namespace Rookie.AssetManagement.Validators
{
    public class UserCreateDtoValidator : BaseValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(m => m.FirstName)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.FirstName)));
            RuleFor(m => m.LastName)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.LastName)));
            RuleFor(m => m.DateOfBirth)
                 .Must(m => m != DateTime.MinValue)
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.DateOfBirth)));
            RuleFor(m => m.DateOfBirth)
                .Must(m => (DateTime.Now.Year - m.Year >= 18))
                .WithMessage(x => string.Format(ErrorTypes.User.DateOfBirthError, nameof(x.DateOfBirth)));
            RuleFor(m =>m.JoinedDate)
                 .Must(m => m != DateTime.MinValue)
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.JoinedDate)));
            RuleFor(m => m)
                .Must(IsJoinedDateWhenUserAgeEnough)
                .WithMessage(x => string.Format(ErrorTypes.User.JoinedDateWhenAgeNotEnough, nameof(x.JoinedDate)));
            RuleFor(m => m)
                .Must(IsTypeInCorrectFormat)
                .WithMessage(ErrorTypes.User.UserTypeError);
            RuleFor(m => m)
                 .Must(IsJoinedDateGreaterThanDOB)
                 .WithMessage(x => string.Format(ErrorTypes.User.JoinedDateError));
            RuleFor(m => m)
                .Must(IsJoinedDayNotSaturdayOrSunday)
                .WithMessage(x => string.Format(ErrorTypes.User.JoinedDateWeekendError));
        }


        private bool IsJoinedDateGreaterThanDOB(UserCreateDto userCreateDto)
        {
            return (userCreateDto.DateOfBirth < userCreateDto.JoinedDate);          
        }
        private bool IsJoinedDateWhenUserAgeEnough(UserCreateDto userCreateDto)
        {
            return (userCreateDto.JoinedDate.Year - userCreateDto.DateOfBirth.Year >= 18);
        }
        private bool IsTypeInCorrectFormat(UserCreateDto userCreateDto)
        {
            return (userCreateDto.Type == UserConstants.Common.AdminRole || userCreateDto.Type == UserConstants.Common.StaffRole);
        }
        private bool IsJoinedDayNotSaturdayOrSunday(UserCreateDto userCreateDto)
        {
            var localTime = userCreateDto.JoinedDate.AddMinutes(-userCreateDto.TimeOffset);
            var test = (localTime.DayOfWeek != DayOfWeek.Saturday && localTime.DayOfWeek != DayOfWeek.Sunday);
            return (localTime.DayOfWeek != DayOfWeek.Saturday && localTime.DayOfWeek != DayOfWeek.Sunday);
        }
    }
}
