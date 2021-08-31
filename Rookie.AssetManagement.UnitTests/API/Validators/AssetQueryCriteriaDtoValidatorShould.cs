using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.EnumDtos;
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
    public class AssetQueryCriteriaDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AssetQueryCriteriaDtoValidator, AssetQueryCriteriaDto> _testRunner;

        public AssetQueryCriteriaDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<AssetQueryCriteriaDtoValidator, AssetQueryCriteriaDto>(new AssetQueryCriteriaDtoValidator());
        }

        [Theory]
        [MemberData(nameof(AssetQueryCriteriaTestData.ValidSortOrder), MemberType = typeof(AssetQueryCriteriaTestData))]
        public void NotHaveErrorWhenSortOrderIsValid(SortOrderEnumDto sortOrder) =>
            _testRunner
            .For(m => m.SortOrder = sortOrder)
            .ShouldNotHaveErrorsFor(m => m.SortOrder);

        [Theory]
        [MemberData(nameof(AssetQueryCriteriaTestData.InvalidSortOrder), MemberType = typeof(AssetQueryCriteriaTestData))]
        public void HaveErrorWhenSortOrderIsInValid(SortOrderEnumDto sortOrder, string errorMessage) =>
            _testRunner
            .For(m => m.SortOrder = sortOrder)
            .ShouldHaveErrorsFor(m => m.SortOrder, errorMessage);
        [Theory]
        [MemberData(nameof(AssetQueryCriteriaTestData.ValidSortColumn), MemberType = typeof(AssetQueryCriteriaTestData))]
        public void NotHaveErrorWhenSortColumnIsValid(string sortColumn) =>
            _testRunner
            .For(m => m.SortColumn = sortColumn)
            .ShouldNotHaveErrorsFor(m => m.SortColumn);
        [Theory]
        [MemberData(nameof(AssetQueryCriteriaTestData.InvalidSortColumn), MemberType = typeof(AssetQueryCriteriaTestData))]
        public void HaveErrorWhenSortColumnIsInvalid(string sortColumn, string errorMessage) =>
            _testRunner
            .For(m => m.SortColumn = sortColumn)
            .ShouldHaveErrorsFor(m => m.SortColumn, errorMessage);
        [Theory]
        [MemberData(nameof(AssetQueryCriteriaTestData.ValidPage), MemberType = typeof(AssetQueryCriteriaTestData))]
        public void NotHaveErrorWhenPageIsValid(int page) =>
            _testRunner
            .For(m => m.Page = page)
            .ShouldNotHaveErrorsFor(m => m.Page);
        [Theory]
        [MemberData(nameof(AssetQueryCriteriaTestData.InvalidPage), MemberType = typeof(AssetQueryCriteriaTestData))]
        public void HaveErrorWhenPageIsInvalid(int page, string errorMessage) =>
            _testRunner
            .For(m => m.Page = page)
            .ShouldHaveErrorsFor(m => m.Page, errorMessage);
    }
}
