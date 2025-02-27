using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class PostingReportView
    {

        public int assignMasterId { get; set; }

        public string refNo { get; set; }

        public DateTime refDate { get; set; }

        public string applicationUserId { get; set; }
        public string createApplicationUserId { get; set; }

        public int? statusId { get; set; }

        public string title { get; set; }
        public string nameEnglish { get; set; }

        public string url { get; set; }
        public string rank { get; set; }

        public string description { get; set; }
        public int[] anulipiIds { get; set; }
        public int? departmentId { get; set; }

        public int?[] anuLipiList { get; set; }
        public string memorandumNo { get; set; }
        public string userRole { get; set; }
        public IEnumerable<AnulipiList> anulipiLists { get; set; }
        public IEnumerable<Assignment> assignments { get; set; }
        public IEnumerable<Assignment> assignmentMas { get; set; }
       // public IEnumerable<AssignmentMaster> assignmentMasters { get; set; }
        public IEnumerable<AssignmentAnulipi> assignmentAnulipis { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }



    }
}
