﻿using Rookie.AssetManagement.Contracts.Enums;
using System;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string Specification { get; set; }
        public DateTime InstalledDate { get; set; }
        public AssetState State { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
