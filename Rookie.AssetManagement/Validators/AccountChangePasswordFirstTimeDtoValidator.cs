using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;

namespace Rookie.AssetManagement.Validators
{
    public class AccountChangePasswordFirstTimeDtoValidator : BaseValidator<AccountChangePasswordFirstTimeDto>
    {
        public AccountChangePasswordFirstTimeDtoValidator()
        {
            RuleFor(m => m.NewPassword)
                .NotNull()
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.NewPassword)));
        }
    }
}
