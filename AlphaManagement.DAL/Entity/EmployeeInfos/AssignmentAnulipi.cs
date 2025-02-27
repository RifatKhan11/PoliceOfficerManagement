using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class AssignmentAnulipi:Base
    {
        public int? assignmentMasterId { get; set; }
        public AssignmentMaster assignmentMaster { get; set; }
        public int? anulipiListId { get; set; }
        public AnulipiList anulipiList { get; set; }

        public int? shortOrder { get; set; }

        public string anulipiText { get; set; }

        public string remarks { get; set; }
    }
}
