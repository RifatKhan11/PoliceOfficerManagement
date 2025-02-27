using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.InternalPosting.Models
{
    public class InternalEmployeeInfoVM
    {
        public int Id { get; set; }
        public int[] employeeId { get; set; }
        public int[] sectionId { get; set; }
    }
}
