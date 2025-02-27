using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
   public class EmployeeReportInfo:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public string type { get; set; }
        public int? year { get; set; }
        public DateTime? date { get; set; }
        public string description { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
    }
}
