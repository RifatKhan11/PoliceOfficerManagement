using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class Photograph:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [Required]
        public string url { get; set; }

        public string remarks { get; set; }

        [Required]
        public string type { get; set; }
    }
}
