using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
   public class MedicalSubCategory:Base
    {
        public string name { get; set; }
        public string nameBn { get; set; }
        public int? sortOrder { get; set; }
        public string description { get; set; }
        public int? medicalMainCategoryId { get; set; }
        public MedicalMainCategory medicalMainCategory { get; set; }
    }
}
