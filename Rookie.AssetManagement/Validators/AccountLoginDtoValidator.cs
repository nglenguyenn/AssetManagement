using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;

namespace Rookie.AssetManagement.Validators
{
	public class AccountLoginDtoValidator : BaseValidator<AccountLoginDto>
	{
        public AccountLoginDtoValidator()
        {
            RuleFor(m => m.Username)
                .NotNull()
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Username)));

            RuleFor(m => m.Password)
                .NotNull()
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Password)));
        }
    }
}
