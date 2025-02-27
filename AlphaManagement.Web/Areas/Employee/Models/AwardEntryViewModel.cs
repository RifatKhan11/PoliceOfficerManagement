using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class AwardEntryViewModel
    {
        public int AwardEntryId { get; set; }
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public string awardName { get; set; }
        public string purpose { get; set; }
        public DateTime awardDate { get; set; }
        //approver
        public string status { get; set; }
        public AwardEntryLn fLang { get; set; }
        public AwardEntry awardEntry { get; set; }
        public IEnumerable<AwardEntry> awardEntrys { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
    }
}
