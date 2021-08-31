using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Enums
{
    public enum AssetState
    {
        Assigned,
        Available,
        NotAvailable,
        Recycled,
        WaitingForRecycling
    }
}
