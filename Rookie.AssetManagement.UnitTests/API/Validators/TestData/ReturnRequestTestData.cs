using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.UnitTests.API.Validators.TestData
{
    public class ReturnRequestTestData
    {
        public static ReturnRequest GetReturnRequest(int id)
        {
            return new ReturnRequest
            {
                Id = id,
                ReturnedDate = new DateTime(2020, 9, 30),
                State = ReturnRequestState.Completed
            };
        }
    }
}
