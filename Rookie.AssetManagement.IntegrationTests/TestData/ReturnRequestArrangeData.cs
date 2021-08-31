using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Contracts.Enums;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public class ReturnRequestArrangeData
    {
        public static async Task InitReturnRequestDataAsync(BaseRepository<ReturnRequest> returnRequestRepository)
        {
            if (!returnRequestRepository.GetAll().Result.Any())
            {
                var returnRequestList = new List<ReturnRequest>()
                {
                    new ReturnRequest()
                    {
                        Id=1,
                        ReturnedDate= DateTime.Now,
                        State=ReturnRequestState.Completed,
                        RequestedByUserId=3,
                        AcceptedByUserId=1,
                        AssignmentId=1,
                    }
                };
                foreach (ReturnRequest returnRequest in returnRequestList)
                {
                    await returnRequestRepository.Add(returnRequest);
                }
            }
        }
    }
}
