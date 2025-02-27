using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class SuggetionViewModel
    {
        public IEnumerable<BranchUnitWiseEmployeesModel> branchUnitWiseEmployees { get; set; }
        public IEnumerable<Section> sections { get; set; }
        public IEnumerable<EnlistedAssignment> enlistedAssignments { get; set; }
    }
}
