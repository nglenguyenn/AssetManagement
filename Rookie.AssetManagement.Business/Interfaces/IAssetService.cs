using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
    public interface IAssetService
    {
        Task<IActionResult> CreateAssetAsync(ClaimsIdentity identity, AssetCreateDto assetCreateDto);
    }
}
