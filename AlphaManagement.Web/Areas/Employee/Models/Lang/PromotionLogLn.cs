using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models.Lang
{
    public class PromotionLogLn
    {
        public string Title { get; set; }
        public string employee { get; set; }
        public string designationNew { get; set; }
        public string designationOld { get; set; }
        public string date { get; set; }
        public string payScale { get; set; }
        public string goNumber { get; set; }
        public string goDate { get; set; }
        public string remark { get; set; }
        public string action { get; set; }
    }
}
