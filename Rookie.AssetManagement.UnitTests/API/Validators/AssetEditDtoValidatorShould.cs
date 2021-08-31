using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
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
    public class AssetEditDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AssetEditDtoValidator, AssetEditDto> _testRunner;

        public AssetEditDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<AssetEditDtoValidator, AssetEditDto>(new AssetEditDtoValidator());
        }

        [Theory]
        [MemberData(nameof(AssetTestData.ValidAssetName), MemberType = typeof(AssetTestData))]
        public void NotHaveErrorWhenAssetNameIsValid(string assetName) =>
          _testRunner
          .For(m => m.AssetName = assetName)
          .ShouldNotHaveErrorsFor(m => m.AssetName);

        [Theory]
        [MemberData(nameof(AssetTestData.InvalidAssetName), MemberType = typeof(AssetTestData))]
        public void HaveErrorWhenAssetNameIsInvalid(string assetName, string errorMessage) =>
          _testRunner
          .For(m => m.AssetName = assetName)
          .ShouldHaveErrorsFor(m => m.AssetName, errorMessage);


        [Theory]
        [MemberData(nameof(AssetTestData.ValidId), MemberType = typeof(AssetTestData))]
        public void NotHaveErrorWhenIdIsValid(int Id) =>
          _testRunner
          .For(m => m.Id = Id)
          .ShouldNotHaveErrorsFor(m => m.Id);

        [Theory]
        [MemberData(nameof(AssetTestData.InvalidId), MemberType = typeof(AssetTestData))]
        public void HaveErrorWhenIdIsInvalid(int Id, string errorMessage) =>
          _testRunner
          .For(m => m.Id = Id)
          .ShouldHaveErrorsFor(m => m.Id, errorMessage);

        [Theory]
        [MemberData(nameof(AssetTestData.ValidInstalledDate), MemberType = typeof(AssetTestData))]
        public void NotHaveErrorWhenInstallDateIsValid(DateTime installedDate) =>
          _testRunner
          .For(m => m.InstalledDate = installedDate)
          .ShouldNotHaveErrorsFor(m => m.InstalledDate);

        [Theory]
        [MemberData(nameof(AssetTestData.InvalidInstalledDate), MemberType = typeof(AssetTestData))]
        public void HaveErrorWhenInstallDateIsInvalid(DateTime installedDate, string errorMessage) =>
          _testRunner
          .For(m => m.InstalledDate = installedDate)
          .ShouldHaveErrorsFor(m => m.InstalledDate, errorMessage);


        [Theory]
        [MemberData(nameof(AssetTestData.ValidSpecification), MemberType = typeof(AssetTestData))]
        public void NotHaveErrorWhenSpecificationIsValid(string specification) =>
          _testRunner
          .For(m => m.Specification = specification)
          .ShouldNotHaveErrorsFor(m => m.Specification);

        [Theory]
        [MemberData(nameof(AssetTestData.InvalidSpecification), MemberType = typeof(AssetTestData))]
        public void HaveErrorWhenSpecificationIsInvalid(string specification, string errorMessage) =>
          _testRunner
          .For(m => m.Specification = specification)
          .ShouldHaveErrorsFor(m => m.Specification, errorMessage);

        [Theory]
        [MemberData(nameof(AssetTestData.ValidState), MemberType = typeof(AssetTestData))]
        public void NotHaveErrorWhenStateIsValid(AssetState state) =>
          _testRunner
          .For(m => m.State = state)
          .ShouldNotHaveErrorsFor(m => m.State);

        [Theory]
        [MemberData(nameof(AssetTestData.InvalidState), MemberType = typeof(AssetTestData))]
        public void HaveErrorWhenStateIsInvalid(AssetState state, string errorMessage) =>
          _testRunner
          .For(m => m.State = state)
          .ShouldHaveErrorsFor(m => m.State, errorMessage);
    }
}
