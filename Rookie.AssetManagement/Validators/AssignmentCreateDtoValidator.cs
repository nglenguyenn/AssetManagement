using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.EnumDtos;
using Rookie.AssetManagement.Contracts.Enums;

namespace Rookie.AssetManagement.Validators
{
    public class AssignmentCreateDtoValidator : BaseValidator<AssignmentCreateDto>
    {
        public AssignmentCreateDtoValidator()
        {
            RuleFor(m => m.AssetId)
                .GreaterThan(0)
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.AssetId)));
            RuleFor(m => m.AssignToId)
                 .GreaterThan(0)
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.AssignToId)));
            RuleFor(m => m.AssignedDate)
                .Must(m => m != DateTime.MinValue)
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.AssignedDate)));
        }
    }
}
