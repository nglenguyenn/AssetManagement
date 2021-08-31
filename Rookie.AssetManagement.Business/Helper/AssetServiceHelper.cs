using Rookie.AssetManagement.Contracts.Constants;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Helper
{
    public static class AssetServiceHelper
    {
        public static IQueryable<Asset> AssetListFilter(
            IQueryable<Asset> assetQuery,
            AssetQueryCriteriaDto queryCriteria)
        {
            var searchValue = queryCriteria.Search;

            assetQuery = assetQuery.Where(entity => entity.Location.Equals(queryCriteria.Location.ToString()));

            if (!string.IsNullOrEmpty(searchValue))
            {
                assetQuery = assetQuery.Where(entity => entity.AssetName.Contains(searchValue) || entity.AssetCode.Contains(searchValue));
            }

            if (queryCriteria.State != null &&
                queryCriteria.State.Count() > 0 &&
                !queryCriteria.State.Any(x => x.ToString() == AssetStateFilter.All))
            {
                assetQuery = assetQuery.Where(entity => queryCriteria.State.Contains(entity.State));
            }

            if (queryCriteria.CategoryName != null &&
                queryCriteria.CategoryName.Count() > 0 &&
                !queryCriteria.CategoryName.Any(x => x == CategoryFilter.All))
            {
                assetQuery = assetQuery.Where(entity => queryCriteria.CategoryName.Any(t => t.Equals(entity.Category.Name)));
            }

            return assetQuery;

        }
    }
}
