using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class EmployeeAssignmentModel
    {
        public SpecialBranchUnit specialBranchUnit { get; set; }

        public int? haveChild { get; set; }

        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
    }
}
