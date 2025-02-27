using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class DivisionWiseEmployeeCountModel
    {
        public int count { get; set; }
        public int? divId { get; set; }
        public string label { get; set; }
        public int? value { get; set; }
    }
}
