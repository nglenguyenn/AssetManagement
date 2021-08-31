using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.EnumDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.Tests.Validations;
using Rookie.AssetManagement.UnitTests.API.Validators.TestData;
using Rookie.AssetManagement.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rookie.AssetManagement.UnitTests.API.Validators
{
    public class AssignmentQueryCriteriaDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AssignmentQueryCriteriaDtoValidator, AssignmentQueryCriteriaDto> _testRunner;

        public AssignmentQueryCriteriaDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<AssignmentQueryCriteriaDtoValidator, AssignmentQueryCriteriaDto>
                (new AssignmentQueryCriteriaDtoValidator());
        }

        [Theory]
        [MemberData(nameof(AssignmentQueryCriteriaTestData.ValidSortOrder), MemberType = typeof(AssignmentQueryCriteriaTestData))]
        public void NotHaveErrorWhenSortOrderIsValid(SortOrderEnumDto sortOrder) =>
            _testRunner
            .For(m => m.SortOrder = sortOrder)
            .ShouldNotHaveErrorsFor(m => m.SortOrder);

        [Theory]
        [MemberData(nameof(AssignmentQueryCriteriaTestData.InvalidSortOrder), MemberType = typeof(AssignmentQueryCriteriaTestData))]
        public void HaveErrorWhenSortOrderIsInValid(SortOrderEnumDto sortOrder, string errorMessage) =>
            _testRunner
            .For(m => m.SortOrder = sortOrder)
            .ShouldHaveErrorsFor(m => m.SortOrder, errorMessage);

        [Theory]
        [MemberData(nameof(AssignmentQueryCriteriaTestData.ValidSortColumn), MemberType = typeof(AssignmentQueryCriteriaTestData))]
        public void NotHaveErrorWhenSortColumnIsValid(string sortColumn) =>
            _testRunner
            .For(m => m.SortColumn = sortColumn)
            .ShouldNotHaveErrorsFor(m => m.SortColumn);

        [Theory]
        [MemberData(nameof(AssignmentQueryCriteriaTestData.InvalidSortColumn), MemberType = typeof(AssignmentQueryCriteriaTestData))]
        public void HaveErrorWhenSortColumnIsInvalid(string sortColumn, string errorMessage) =>
            _testRunner
            .For(m => m.SortColumn = sortColumn)
            .ShouldHaveErrorsFor(m => m.SortColumn, errorMessage);

        [Theory]
        [MemberData(nameof(AssignmentQueryCriteriaTestData.ValidPage), MemberType = typeof(AssignmentQueryCriteriaTestData))]
        public void NotHaveErrorWhenPageIsValid(int page) =>
            _testRunner
            .For(m => m.Page = page)
            .ShouldNotHaveErrorsFor(m => m.Page);

        [Theory]
        [MemberData(nameof(AssignmentQueryCriteriaTestData.InvalidPage), MemberType = typeof(AssignmentQueryCriteriaTestData))]
        public void HaveErrorWhenPageIsInvalid(int page, string errorMessage) =>
            _testRunner
            .For(m => m.Page = page)
            .ShouldHaveErrorsFor(m => m.Page, errorMessage);

        [Theory]
        [MemberData(nameof(AssignmentQueryCriteriaTestData.ValidState), MemberType = typeof(AssignmentQueryCriteriaTestData))]
        public void NotHaveErrorWhenStateIsValid(AssignmentState[] states) =>
            _testRunner
            .For(m => m.States = states)
            .ShouldNotHaveErrorsFor(m => m.States);

        [Theory]
        [MemberData(nameof(AssignmentQueryCriteriaTestData.InvalidState), MemberType = typeof(AssignmentQueryCriteriaTestData))]
        public void HaveErrorWhenStateIsInvalid(AssignmentState[] states, string errorMessage) =>
            _testRunner
            .For(m => m.States = states)
            .ShouldHaveErrorsFor(m => m.States, errorMessage);
    }
}
