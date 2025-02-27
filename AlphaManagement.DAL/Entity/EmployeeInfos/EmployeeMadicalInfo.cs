using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
   public class EmployeeMadicalInfo:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public int? medicalSubCategoryId { get; set; }
        public MedicalSubCategory medicalSubCategory { get; set; }
        public DateTime? date { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
    }
}
