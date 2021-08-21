using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
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
    public class UserQueryCriteriaDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<UserQueryCriteriaDtoValidator, UserQueryCriteriaDto> _testRunner;

        public UserQueryCriteriaDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<UserQueryCriteriaDtoValidator, UserQueryCriteriaDto>(new UserQueryCriteriaDtoValidator());
        }

        [Theory]
        [MemberData(nameof(UserQueryCriteriaTestData.ValidSortOrder), MemberType = typeof(UserQueryCriteriaTestData))]
        public void NotHaveErrorWhenSortOrderIsValid(SortOrderEnumDto sortOrder) =>
            _testRunner
            .For(m => m.SortOrder = sortOrder)
            .ShouldNotHaveErrorsFor(m => m.SortOrder);

        [Theory]
        [MemberData(nameof(UserQueryCriteriaTestData.InvalidSortOrder), MemberType = typeof(UserQueryCriteriaTestData))]
        public void HaveErrorWhenSortOrderIsInvalid(SortOrderEnumDto sortOrder, string errorMessage) =>
            _testRunner
            .For(m => m.SortOrder = sortOrder)
            .ShouldHaveErrorsFor(m => m.SortOrder, errorMessage);

        [Theory]
        [MemberData(nameof(UserQueryCriteriaTestData.ValidSortColumn), MemberType = typeof(UserQueryCriteriaTestData))]
        public void NotHaveErrorWhenSortColumnIsValid(string sortColumn) =>
            _testRunner
            .For(m => m.SortColumn = sortColumn)
            .ShouldNotHaveErrorsFor(m => m.SortColumn);

        [Theory]
        [MemberData(nameof(UserQueryCriteriaTestData.InvalidSortColumn), MemberType = typeof(UserQueryCriteriaTestData))]
        public void HaveErrorWhenSortColumnIsInvalid(string sortColumn, string errorMessage) =>
            _testRunner
            .For(m => m.SortColumn = sortColumn)
            .ShouldHaveErrorsFor(m => m.SortColumn, errorMessage);

        [Theory]
        [MemberData(nameof(UserQueryCriteriaTestData.ValidPage), MemberType = typeof(UserQueryCriteriaTestData))]
        public void NotHaveErrorWhenPageIsValid(int page) =>
            _testRunner
            .For(m => m.Page = page)
            .ShouldNotHaveErrorsFor(m => m.Page);

        [Theory]
        [MemberData(nameof(UserQueryCriteriaTestData.InvalidPage), MemberType = typeof(UserQueryCriteriaTestData))]
        public void HaveErrorWhenPageIsInvalid(int page, string errorMessage) =>
            _testRunner
            .For(m => m.Page = page)
            .ShouldHaveErrorsFor(m => m.Page, errorMessage);
    }
}
