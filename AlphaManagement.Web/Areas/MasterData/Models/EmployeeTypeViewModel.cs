using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class EmployeeTypeViewModel
    {
        public int EmployeeTypeId { get; set; }
        public string empType { get; set; }
        public string empTypeBn { get; set; }
        public string shortName { get; set; }
        public EmployeeTypeLn fLang { get; set; }
        public EmployeeType employeeType { get; set; }
        public IEnumerable<EmployeeType> employeeTypes { get; set; }
    }
}
