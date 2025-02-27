using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models
{
    public class AspnetAndEmployeeModel
    {
        public string UserName { get; set; }
        public string ID { get; set; }
    
        public string Email { get; set; }

        public string Designation { get; set; }
   
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string NID { get; set; }
        public int? isActive { get; set; }
        public string roleId { get; set; }
        public string unit { get; set; }

        public int? empId { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public int? rankId { get; set; }
        public int? designationId { get; set; }
    }
}
