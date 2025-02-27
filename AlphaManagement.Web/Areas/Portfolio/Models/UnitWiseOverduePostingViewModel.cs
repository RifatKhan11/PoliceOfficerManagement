using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Portfolio.Models
{
    public class UnitWiseOverduePostingViewModel
    {
        public List<string> branchUnitName { get; set; }
        public List<int?> totalEmployee { get; set; }
    }
}
