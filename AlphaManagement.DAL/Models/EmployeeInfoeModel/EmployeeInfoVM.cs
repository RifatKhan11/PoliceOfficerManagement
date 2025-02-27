using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class EmployeeInfoVM
    {
        public int? Id { get; set; }
        public string empCode { get; set; }
        public string empName { get; set; }
        public string rankName { get; set; }
        public string imageUrl { get; set; }
    }
}
