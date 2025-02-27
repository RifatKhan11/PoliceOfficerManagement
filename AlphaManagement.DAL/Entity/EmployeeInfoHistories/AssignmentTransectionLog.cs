using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
    public class AssignmentTransectionLog : Base
    {
        public String UpdateUserId { get; set; }
        public ApplicationUser UpdateUser { get; set; }

        public int? assignmentId { get; set; }
        public Assignment assignment { get; set; }

        public int? oldSectionId { get; set; }
        public Section oldSection { get; set; }

        public int? newSectionId { get; set; }
        public Section newSection { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

    }
}
