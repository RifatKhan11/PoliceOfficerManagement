using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class PostingSaveViewModel
    {
        public string refNumber { get; set; }
        public int assignMasterId { get; set; }
        public int status { get; set; }
    }
}
