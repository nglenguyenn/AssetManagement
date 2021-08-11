using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Contracts.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
	public interface IAccountService
	{
		Task<IActionResult> Login([FromBody] AccountLoginDto accountLoginDto);
	}
}
