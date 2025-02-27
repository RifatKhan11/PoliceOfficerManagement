using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
   public class EnlistedAssignmentMaster:Base
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string refNo { get; set; }
        public DateTime? refDate { get; set; }
        public int? statusId { get; set; }
        public int typeId { get; set; }//1=enlisted,2=hold
        [Column(TypeName = "NVARCHAR(550)")]
        public string remarks { get; set; }
        
        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
