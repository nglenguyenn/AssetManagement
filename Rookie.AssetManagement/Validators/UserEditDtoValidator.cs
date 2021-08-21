using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using System;
using FluentValidation;

namespace Rookie.AssetManagement.Validators
{
    public class UserEditDtoValidator :BaseValidator<UserEditDto>
    {
        public UserEditDtoValidator()
    {
        RuleFor(m => m.DateOfBirth)
             .Must(m => m != DateTime.MinValue)
             .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.DateOfBirth)));
        RuleFor(m => m.DateOfBirth)
            .Must(m => (DateTime.Now.Year - m.Year >= 18))
            .WithMessage(x => string.Format(ErrorTypes.User.DateOfBirthError, nameof(x.DateOfBirth)));
        RuleFor(m => m.JoinedDate)
             .Must(m => m != DateTime.MinValue)
             .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.JoinedDate)));
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


    private bool IsJoinedDateGreaterThanDOB(UserEditDto UserEditDto)
    {
        return (UserEditDto.DateOfBirth < UserEditDto.JoinedDate);
    }
    private bool IsTypeInCorrectFormat(UserEditDto UserEditDto)
    {
        return (UserEditDto.Type == UserConstants.Common.AdminRole || UserEditDto.Type == UserConstants.Common.StaffRole);
    }
    private bool IsJoinedDayNotSaturdayOrSunday(UserEditDto UserEditDto)
    {
        var localTime = UserEditDto.JoinedDate.AddMinutes(-UserEditDto.TimeOffset);
        var test = (localTime.DayOfWeek != DayOfWeek.Saturday && localTime.DayOfWeek != DayOfWeek.Sunday);
        return (localTime.DayOfWeek != DayOfWeek.Saturday && localTime.DayOfWeek != DayOfWeek.Sunday);
    }
    
    }
}
