using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Validators
{
    public class AssetCreateDtoValidator : BaseValidator<AssetCreateDto>
    {
        public AssetCreateDtoValidator()
        {
            RuleFor(m => m.AssetName)
                 .NotEmpty()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.AssetName)));
            RuleFor(m => m.CategoryId)
                 .Must(m => m > 0)
                 .WithMessage(x => string.Format(ErrorTypes.Common.NumberGreaterThanError, nameof(x.CategoryId), 0));
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
