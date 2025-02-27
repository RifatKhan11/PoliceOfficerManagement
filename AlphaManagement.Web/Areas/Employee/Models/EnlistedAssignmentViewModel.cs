using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EnlistedAssignmentViewModel
    {
        public ApplicationUser applicationUser { get; set; }
        public EmployeeInfo employee { get; set; }
        public IEnumerable<EnlistedAssignment> enlistedAssignments { get; set; }
        public IEnumerable<EnlistedAssignment> frezzAssignments { get; set; }
        public IEnumerable<EnlistedViewModel> enlistedMasterAssignments { get; set; }
        public IEnumerable<EnlistedViewModel> frezzMasterAssignments { get; set; }
    }
}
