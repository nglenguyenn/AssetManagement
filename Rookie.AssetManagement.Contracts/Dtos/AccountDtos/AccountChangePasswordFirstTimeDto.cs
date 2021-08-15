using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AccountDtos
{
    public class AccountChangePasswordFirstTimeDto
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
