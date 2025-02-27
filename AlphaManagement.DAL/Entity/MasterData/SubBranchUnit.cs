using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class SubBranchUnit:Base
    {
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string branchUnitName { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string branchUnitNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
