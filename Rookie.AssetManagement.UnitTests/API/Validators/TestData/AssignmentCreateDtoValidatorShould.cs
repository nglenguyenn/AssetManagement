using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Tests.Validations;
using Rookie.AssetManagement.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rookie.AssetManagement.UnitTests.API.Validators.TestData
{
    public class AssignmentCreateDtoValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<AssignmentCreateDtoValidator, AssignmentCreateDto> _testRunner;

        public AssignmentCreateDtoValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<AssignmentCreateDtoValidator, AssignmentCreateDto>(new AssignmentCreateDtoValidator());
        }
        [Theory]
        [MemberData(nameof(AssignmentTestData.ValidAssetId), MemberType = typeof(AssignmentTestData))]
        public void NotHaveErrorWhenAssetIdIsValid(int AssetId) =>
          _testRunner
          .For(m => m.AssetId = AssetId)
          .ShouldNotHaveErrorsFor(m => m.AssetId);

        [Theory]
        [MemberData(nameof(AssignmentTestData.InvalidAssetId), MemberType = typeof(AssignmentTestData))]
        public void HaveErrorWhenAssetIdIsInvalid(int AssetId, string errorMessage) =>
          _testRunner
          .For(m => m.AssetId = AssetId)
          .ShouldHaveErrorsFor(m => m.AssetId, errorMessage);
        [Theory]
        [MemberData(nameof(AssignmentTestData.ValidAssignToId), MemberType = typeof(AssignmentTestData))]
        public void NotHaveErrorWhenAssignToIdIsValid(int AssignToId) =>
          _testRunner
          .For(m => m.AssignToId = AssignToId)
          .ShouldNotHaveErrorsFor(m => m.AssignToId);

        [Theory]
        [MemberData(nameof(AssignmentTestData.InvalidAssignToId), MemberType = typeof(AssignmentTestData))]
        public void HaveErrorWhenAssignToIdIsInvalid(int AssignToId, string errorMessage) =>
          _testRunner
          .For(m => m.AssignToId = AssignToId)
          .ShouldHaveErrorsFor(m => m.AssignToId, errorMessage);
        [Theory]
        [MemberData(nameof(AssignmentTestData.ValidDate), MemberType = typeof(AssignmentTestData))]
        public void NotHaveErrorWhenAssignedDateIsValid(DateTime AssignedDate) =>
          _testRunner
          .For(m => m.AssignedDate = AssignedDate)
          .ShouldNotHaveErrorsFor(m => m.AssignedDate);
    }
}
