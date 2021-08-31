using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;

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
            RuleFor(m => m)
                .Must(IsNewPasswordDifferrent)
                .WithMessage(x => string.Format(ErrorTypes.User.NewPasswordIsNotDifferent));
        }

        private bool IsNewPasswordDifferrent(AccountChangePasswordDto accountChangePasswordDto )
        {
            return (accountChangePasswordDto.OldPassword != accountChangePasswordDto.NewPassword);
        }
    }
}
