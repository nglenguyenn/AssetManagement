using System.Collections.Generic;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryCode { get; set; }
        public string Name { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}
