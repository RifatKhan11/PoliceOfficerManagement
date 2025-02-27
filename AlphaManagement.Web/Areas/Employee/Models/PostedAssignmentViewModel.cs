using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class PostedAssignmentViewModel
    {
        public string refNo { get; set; }
        public string applicationUserId { get; set; }
        public int? statusId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string nameEnglish { get; set; }
    }
}
