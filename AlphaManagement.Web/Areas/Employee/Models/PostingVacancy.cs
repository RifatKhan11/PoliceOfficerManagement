using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class PostingVacancy
    {
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<SpecialBranchUnit> ParentBranchUnits { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<BCSBatch> batches { get; set; }
        public IEnumerable<Department> sections { get; set; }
        public IEnumerable<Assignment> assignments { get; set; }
    }
}
