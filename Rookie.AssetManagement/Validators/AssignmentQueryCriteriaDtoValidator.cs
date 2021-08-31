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
    public class AssignmentQueryCriteriaDtoValidator : BaseValidator<AssignmentQueryCriteriaDto>
    {
        public AssignmentQueryCriteriaDtoValidator()
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
            RuleFor(m => m.States)
                .Must(IsValidState)
                .WithMessage(x => string.Format(ErrorTypes.Assignment.StateFilter, nameof(x.States)));
        }

        private bool IsValidState(AssignmentState[] states)
        {
            if (states != null)
            {
                if(states.Contains(AssignmentState.Completed) || states.Contains(AssignmentState.Declined))
                    return false;
            }
            return true;
        }
    }
}
