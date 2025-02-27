using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class CheckBranchViewModel
    {
        public int SBUId { get; set; }
        public string reason { get; set; }
        public int? branchId { get; set; }
        public int? isDefault { get; set; }
    }
}
