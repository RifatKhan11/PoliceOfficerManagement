using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models
{
   public class SuggestedBranched
    {
        public int? unitId { get; set; }
        public int? specialBranchUnitId { get; set; }
        public int? isParent { get; set; }
        public int? numOfPost { get; set; }
        public int? vacantPost { get; set; }
        public int? totalEmployee { get; set; }
        public string subUnit { get; set; }
        public string mainUnit { get; set; }
    }
}
