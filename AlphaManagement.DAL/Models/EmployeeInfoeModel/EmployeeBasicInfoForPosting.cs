using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EmployeeBasicInfoForPosting
    {
        public string name { get; set; }
        public string picture { get; set; }
        public string rank { get; set; }
        public string bp { get; set; }
        public string priviousPosting { get; set; }
        public string currentPosting { get; set; }
        public string HomeDistrict { get; set; }
        public string SpouseHomeDistrict { get; set; }
        public string Education { get; set; }
        public string lastPromotionDate { get; set; }
        public List<string> assignments { get; set; }
    }

    
}
