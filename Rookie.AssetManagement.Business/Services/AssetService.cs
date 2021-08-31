using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Helper;
using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Services
{
    public class AssetService : ControllerBase, IAssetService
    {
        private readonly UserManager<User> _userManager;
        private readonly IBaseRepository<Asset> _assetRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IBaseRepository<Assignment> _assignmentRepository;
        private readonly IMapper _mapper;
        public AssetService(
            UserManager<User> userManager,
            IBaseRepository<Asset> assetRepository,
            IBaseRepository<Category> categoryRepository,
            IBaseRepository<Assignment> assignmentRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _assetRepository = assetRepository;
            _categoryRepository = categoryRepository;
            _assignmentRepository = assignmentRepository;
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

		public async Task<IActionResult> EditAssetAsync(AssetEditDto assetEditDto)
		{
            Asset currentAsset = await _assetRepository.GetById(assetEditDto.Id);

            if (currentAsset == null)
                return NotFound(ErrorTypes.Asset.AssetNotFoundError);

            _mapper.Map(assetEditDto, currentAsset);

            Asset result = await _assetRepository.Update(currentAsset);

            AssetResponseDto assetResponseDto = _mapper.Map<AssetResponseDto>(result);

            return Ok(assetResponseDto);
        }
	
	    public async Task<IActionResult> GetAssetListAsync(ClaimsIdentity identity, AssetQueryCriteriaDto queryCriteria, CancellationToken cancellationToken)
        {
            var claimsId = identity.FindFirst(UserClaims.Id).Value;

            if (claimsId == null)
                return Unauthorized(ErrorTypes.User.TokenNotFoundError);

            var actionHandlerUser = await _userManager.FindByIdAsync(claimsId);
            if (actionHandlerUser == null || actionHandlerUser.Type != UserConstants.Common.AdminRole)
                return Unauthorized();

            queryCriteria.Location = actionHandlerUser.Location;

            var assetQuery = AssetServiceHelper.AssetListFilter(_assetRepository.Entities.AsQueryable(), queryCriteria);

            var paginateModel= await assetQuery.PaginateAsync(queryCriteria, cancellationToken);

            var assetDtos = _mapper.Map<IEnumerable<AssetDto>>(paginateModel.Items);

            return Ok(new PagedResponseModel<AssetDto>
            {
                CurrentPage = paginateModel.CurrentPage,
                TotalPages = paginateModel.TotalPages,
                TotalItems = paginateModel.TotalItems,
                Items = assetDtos
            });
    	}

	    public async Task<AssetDetailDto> GetAssetByIdAsync(int id )
        {
            var asset = await _assetRepository.Entities
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (asset == null)
            {
                throw new NotFoundException($"Asset {id} is not found");
            }
            var assetDto = _mapper.Map<AssetDetailDto>(asset);

            return assetDto;
        }


        public async Task<IEnumerable<AssetHistoryDto>> GetListAssetHistoryAsync(int id)
        {
            var asset = await _assetRepository.Entities
                .FirstOrDefaultAsync(x => x.Id == id);

            if (asset == null)
            {
                throw new NotFoundException($"Asset {id} is not found");
            }

            var assetHistory = await _assignmentRepository.Entities
                .Where(x => x.AssetId == id && x.State == AssignmentState.Completed)               
                .Include(x => x.ReturnRequest)
                .Include(x => x.AssignBy)
                .Include(x => x.AssignTo)
                .OrderByDescending(assetHistoryNewest => assetHistoryNewest.AssignedDate)
                .Take(5)
                .ToListAsync();

            var assetHistoryDto = _mapper.Map<IEnumerable<AssetHistoryDto>>(assetHistory);

            return assetHistoryDto;
        }
   }
}
