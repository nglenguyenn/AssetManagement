using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Services
{
    public class AssetService : ControllerBase, IAssetService
    {
        private readonly UserManager<User> _userManager;
        private readonly IBaseRepository<Asset> _assetRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public AssetService(
            UserManager<User> userManager,
            IBaseRepository<Asset> assetRepository, 
            IBaseRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _assetRepository = assetRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateAssetAsync(ClaimsIdentity identity, AssetCreateDto assetCreateDto)
        {
            if (assetCreateDto == null)
                return BadRequest();

            var id = identity.FindFirst(UserClaims.Id).Value;
            if (id == null)
                return Unauthorized("Token not found");

            if (identity.FindFirst(UserClaims.Role).Value != UserConstants.Common.AdminRole)
                return Unauthorized();

            string categoryCode = _categoryRepository.Entities.Where(c => c.Id == assetCreateDto.CategoryId).Select(c => c.CategoryCode).First().ToString();
            StringBuilder regexString = new StringBuilder();
            regexString.Append(@"^").Append(categoryCode).Append(@"\d{6}");
            StringBuilder newAssetCode = new StringBuilder();
            if (_assetRepository.Entities.Where(a => a.CategoryId == assetCreateDto.CategoryId).Any())
            {
                var latestAssetCode = _assetRepository.Entities.AsEnumerable()
                                .Where(a => Regex.IsMatch(a.AssetCode, regexString.ToString()))
                                .OrderByDescending(a => a.AssetCode)
                                .Select(a => a.AssetCode).First().Substring(2);
                newAssetCode.Append(categoryCode).Append((int.Parse(latestAssetCode) + 1).ToString(UserConstants.Common.SixNumbersFormat));
            } else
            {
                newAssetCode.Append(categoryCode).Append((1).ToString(UserConstants.Common.SixNumbersFormat));
            }
            Asset newAsset = new Asset();
            _mapper.Map(assetCreateDto, newAsset);
            newAsset.AssetCode = newAssetCode.ToString();
            newAsset.Location = identity.FindFirst(UserClaims.Location).Value;
            var addedAsset = await _assetRepository.Add(newAsset);
            var newAssetResponse = _mapper
                .Map<AssetResponseDto>(addedAsset);
             
            return Ok(newAssetResponse);
        }
    }
}
