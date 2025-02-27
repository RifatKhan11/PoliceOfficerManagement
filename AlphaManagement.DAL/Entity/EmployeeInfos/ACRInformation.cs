using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class ACRInformation:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public int? year { get; set; }
        public decimal? marks { get; set; }
        public int? statusId { get; set; }
    }
}
