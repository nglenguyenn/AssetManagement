namespace Rookie.AssetManagement.Contracts.Dtos.AccountDtos
{
      public class AccountResponse
      {
            public string Status { get; set; } = null;
            public int Id { get; set; } = -1;
            public string Token { get; set; } = null;
            public string UserName { get; set; } = null;
            public string Role { get; set; } = null;
            public string FullName { get; set; } = null;
            public string StaffCode { get; set; } = null;
            public string Location { get; set; } = null;
            public bool IsConfirmed { get; set; } = false;
      }
}
