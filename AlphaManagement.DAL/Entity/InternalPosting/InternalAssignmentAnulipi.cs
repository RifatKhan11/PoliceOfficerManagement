using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.InternalPosting
{
    public class InternalAssignmentAnulipi:Base
    {
        public int? assignmentMasterId { get; set; }
        public InternalAssignmentMaster assignmentMaster { get; set; }
        public int? anulipiListId { get; set; }
        public InternalAnulipiList anulipiList { get; set; }

        public int? shortOrder { get; set; }

        public string anulipiText { get; set; }

        public string remarks { get; set; }
    }
}
