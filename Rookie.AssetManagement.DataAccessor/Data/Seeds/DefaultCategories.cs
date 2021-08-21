using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.DataAccessor.Entities;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
    public static class DefaultCategories
    {
        public static void SeedCategoryData(this ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryCode= "LA",
                    Name="Laptop",
                },
                new Category
                {
                    Id = 2,
                    CategoryCode = "MO",
                    Name = "Monitor",
                },
                new Category
                {
                    Id = 3,
                    CategoryCode = "PC",
                    Name = "Personal Computer",
                });        
        }
    }
}
