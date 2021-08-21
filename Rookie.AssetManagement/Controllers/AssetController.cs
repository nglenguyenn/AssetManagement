using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    }
}
