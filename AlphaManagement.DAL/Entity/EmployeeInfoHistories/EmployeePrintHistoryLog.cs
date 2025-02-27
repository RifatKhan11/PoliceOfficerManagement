using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
   public class EmployeePrintHistoryLog:Base
    {
        [MaxLength(250)]
        public string userId { get; set; }
        [MaxLength(250)]
        public DateTime logTime { get; set; }
        public int? status { get; set; }
        [MaxLength(250)]
        public string ipAddress { get; set; }
        [MaxLength(250)]
        public string browserName { get; set; }
        [MaxLength(250)]
        public string pcName { get; set; }

        public int? employeeInfosId { get; set; }
        public EmployeeInfo employeeInfos { get; set; }

        public string printCode { get; set; }

        public string fileName { get; set; }
    }
}
