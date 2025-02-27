using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class PostingViewModel
    {
        public int[] sectionId { get; set; }
        public int[] employeeId { get; set; }
        public int[] frezzEmployeeId { get; set; }
        public int[] rankId { get; set; }
        public int AssignmentId { get; set; }
        public int enlistedId { get; set; }
        public string referenceNum { get; set; }
    }
}
