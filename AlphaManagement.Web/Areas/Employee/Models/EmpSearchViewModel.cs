using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EmpSearchViewModel
    {
        public string BranchName { get; set; }
        public string nameEnglish { get; set; }
        public string empCode { get; set; }
        public IEnumerable<EmployeeInfo>  employeeInfos { get; set; }
        public IEnumerable<AddressInformation> addressInformation { get; set; }
        public IEnumerable<SpecialBranchUnit>  specialBranchUnits { get; set; }
    }
}
