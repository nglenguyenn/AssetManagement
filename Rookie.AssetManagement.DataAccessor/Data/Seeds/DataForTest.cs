using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using Rookie.AssetManagement.Contracts.Enums;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
    public static class DataForTest
    {
        public static void SeedDataForTest(this ModelBuilder builder)
        {
            builder.Entity<Asset>().HasData(
                new Asset
                {
                    Id = 1,
                    AssetCode = "LA000001",
                    AssetName = "Macbook Pro 2021",
                    Specification = "Macbook Pro 2021",
                    InstalledDate = DateTime.Now,
                    CategoryId = 1,
                    State = AssetState.Available,
                    Location = "HN",
                });
            builder.Entity<Assignment>().HasData(
                new Assignment
                {
                    Id = 1,
                    AssignedDate = DateTime.Now,
                    State = AssignmentState.Completed,
                    Note = "Test",
                    AssetId = 1,
                    AssignById = 2,
                    AssignToId = 3,
                });
            builder.Entity<ReturnRequest>().HasData(
                new ReturnRequest
                {
                    Id = 1,
                    ReturnedDate = DateTime.Now,
                    State = ReturnRequestState.Completed,
                    RequestedByUserId = 3,
                    AcceptedByUserId = 2,
                    AssignmentId = 1,
                });



        }
    }
}
