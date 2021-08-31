using FluentValidation;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Validators
{
    public class AssetQueryCriteriaDtoValidator : BaseValidator<AssetQueryCriteriaDto>
    {
        public AssetQueryCriteriaDtoValidator()
        {
            RuleFor(m => m.SortOrder)
                .IsInEnum()
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.SortOrder)));
            RuleFor(m => m.SortColumn)
                .NotNull()
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.SortColumn)));
            RuleFor(m => m.Page)
                .GreaterThan(0)
                .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Page))); 

        }
    }
}
