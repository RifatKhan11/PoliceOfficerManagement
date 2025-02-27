using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class SpecialBranchUnitViewModel
    {
        public int branchId { get; set; }
        public string branchUnitName { get; set; }
        public string branchUnitNameBN { get; set; }
        public string branchCode { get; set; }

        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public int? isParent { get; set; }
        public int? shortOrder { get; set; }
        public int? isdefault { get; set; }
        public int? totalPost { get; set; }
        public int? totalBlankPost { get; set; }
    }
}
