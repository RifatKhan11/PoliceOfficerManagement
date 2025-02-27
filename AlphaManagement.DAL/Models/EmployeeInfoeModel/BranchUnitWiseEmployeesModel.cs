using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class BranchUnitWiseEmployeesModel
    {
      
        public virtual IEnumerable<EmployeeInfo> unitEmployees { get; set; }
        public int? unitTotalPost { get; set; }
        public int? unitTotalBlankPost { get; set; }
      
        public int Id { get; set; }
        public bool hasSubUnit { get; set; }
        
        //public string branchUnitName { get; set; }
        //public string branchUnitNameBN { get; set; }
        //public string branchCode { get; set; }
        public int? isParent { get; set; }
        public SpecialBranchUnit parentSpecialBranchUnit { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }
        //public int? shortOrder { get; set; }
        //public int? isdefault { get; set; }

        public virtual IEnumerable<EmployeeInfo> subUnitEmployees { get; set; }
        public virtual IEnumerable<EducationalQualification> EducationalQualifications { get; set; }
        public int? subUnitTotalPost { get; set; }
        public int? subUnitTotalBlankPost { get; set; }
        public int subUnitId { get; set; }
    }
}
