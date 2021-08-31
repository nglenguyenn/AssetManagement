using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using System;

namespace Rookie.AssetManagement.Validators
{
	public class AssetEditDtoValidator : BaseValidator<AssetEditDto>
	{
        public AssetEditDtoValidator()
        {
            RuleFor(m => m.Id)
                 .NotEmpty()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Id)));
            RuleFor(m => m.AssetName)
                 .NotEmpty()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.AssetName)));
            RuleFor(m => m.Specification)
                 .NotEmpty()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Specification)));
            RuleFor(m => m.InstalledDate)
                .Must(m => m != DateTime.MinValue)
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.InstalledDate)));
            RuleFor(m => m.State)
                .IsInEnum()
                .WithMessage(x => string.Format(ErrorTypes.Common.InvalidEnumError, x.State, nameof(x.State)));
        }
    }
}
