using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AccountDtos
{
	public class AccountResponse
	{
		public string Status { get; set; } = null;
		public int Id { get; set; } = -1;
		public string Token { get; set; } = null;
		public string Username { get; set; } = null;
		public string Role { get; set; } = null;
		public string Fullname { get; set; } = null;
		public string StaffCode { get; set; } = null;
		public string Location { get; set; } = null;
		public bool isConfirmed { get; set; } = false;
	}
}
