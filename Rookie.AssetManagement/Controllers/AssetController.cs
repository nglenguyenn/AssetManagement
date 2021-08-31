using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsset([FromBody] AssetCreateDto assetCreateDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _assetService.CreateAssetAsync(identity, assetCreateDto);
        }

        [Authorize("Admin")]
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditAsset([FromBody] AssetEditDto assetEditDto)
        {
            return await _assetService.EditAssetAsync(assetEditDto);
        }
        [Authorize]
        [HttpGet]
        [Route("getList")]
        public async Task<IActionResult> GetAssetList([FromQuery] AssetQueryCriteriaDto queryCriteria, CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await _assetService.GetAssetListAsync(identity, queryCriteria, cancellationToken); 
        }

	    [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetByIdAsync(int id)
        {
            var assetResponses = await _assetService.GetAssetByIdAsync(id);
            return Ok(assetResponses); 
        }

        [Authorize]
        [HttpGet("history")]
        public async Task<IActionResult> GetListAssetHistoryAsync(int id)
        {
            var assetHistory = await _assetService.GetListAssetHistoryAsync(id);
            return Ok(assetHistory);
        }
    }
}
