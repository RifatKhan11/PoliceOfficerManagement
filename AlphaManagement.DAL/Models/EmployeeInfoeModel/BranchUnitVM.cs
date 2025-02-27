using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class BranchUnitVM
    {
        public int? Id { get; set; }
        public string branchUnitName { get; set; }
        public string branchUnitNameBN { get; set; }
        public string branchCode { get; set; }

        public int? isParent { get; set; }

        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }


        public int? shortOrder { get; set; }
        public int? isdefault { get; set; }

        public int? totalPost { get; set; }

        public int? totalBlankPost { get; set; }

        public virtual IEnumerable<EmployeeInfo> EmployeeInfos { get; set; }

        
    }
}
