using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
    public interface IAssetService
    {
        Task<IActionResult> CreateAssetAsync(ClaimsIdentity identity, AssetCreateDto assetCreateDto);
        Task<IActionResult> EditAssetAsync(AssetEditDto assetEditDto);
        Task<IActionResult> GetAssetListAsync(ClaimsIdentity identity, AssetQueryCriteriaDto queryCriteria, CancellationToken cancellationToken);
	    Task<AssetDetailDto> GetAssetByIdAsync(int id);
        Task<IEnumerable<AssetHistoryDto>> GetListAssetHistoryAsync(int id);
    }
}
