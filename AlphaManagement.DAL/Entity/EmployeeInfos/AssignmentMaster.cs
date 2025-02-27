using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class AssignmentMaster:Base
    {
        [Column(TypeName = "NVARCHAR(150)")]
        public string refNo { get; set; }
        public DateTime refDate { get; set; }
        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public int? statusId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string memorandumNo { get; set; }
        public string noteSheetTitle { get; set; }
        public string noteSheetDescription { get; set; }

    }
}
