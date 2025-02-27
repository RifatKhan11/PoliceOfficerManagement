using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.InternalPosting
{
   public class InternalEnlistedAssignmentMaster:Base
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string refNo { get; set; }
        public DateTime? refDate { get; set; }
        public int? statusId { get; set; }//1=created,2=Ongoing,3=IGP Locked
        public int typeId { get; set; }//1=enlisted,2=hold
        [Column(TypeName = "NVARCHAR(550)")]
        public string remarks { get; set; }
        public int? assignmentTypeId { get; set; }//1=Regular,2=Additional
        public InternalAssignmentType assignmentType { get; set; }

        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
