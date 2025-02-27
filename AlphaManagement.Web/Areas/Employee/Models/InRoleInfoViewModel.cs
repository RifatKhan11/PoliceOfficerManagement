using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class InRoleInfoViewModel
    {
        public int assignMasterId { get; set; }
        public string refNo { get; set; }
        public int[] empId { get; set; }
        public string[] empCode { get; set; }
        public int[] empRank { get; set; }
    }
}
