using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Api.Models
{
    public class MasterDataViewModel
    {
        public IEnumerable<SpecialBranchUnit> branchUnits { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
        public IEnumerable<BCSBatch> bCSBatches { get; set; }
    }
}
