using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class EnlistedViewModel
    {
        public EmployeeInfo employeeInfo { get; set; }
        public IEnumerable<EnlistedAssignment> enlistedEmployee { get; set; }
        public EnlistedAssignmentMaster enlistedAssignmentMasters { get; set; }
    }
}
