using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;

namespace Rookie.AssetManagement.Validators
{
    public class AccountChangePasswordDtoValidator : BaseValidator<AccountChangePasswordDto>
    {
        public AccountChangePasswordDtoValidator()
        {
            RuleFor(m => m.OldPassword)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.OldPassword)));

            RuleFor(m => m.NewPassword)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.NewPassword)));
        }
    }
}
