using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class EnlistedAssignment:Base
    {
        
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }
        public int? rankId { get; set; }
        public Rank rank { get; set; }
        public int? statusId { get; set; }//1=enlisted,2=freezz
        public int typeId { get; set; }//1=enlisted,2=hold
        [Column(TypeName = "NVARCHAR(550)")]
        public string remarks { get; set; }

        public int? enlistedAssignmentMasterId { get; set; }   
        public EnlistedAssignmentMaster enlistedAssignmentMaster { get; set; }
    }
}
