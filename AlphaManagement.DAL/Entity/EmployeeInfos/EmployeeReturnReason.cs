using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
   public class EmployeeReturnReason:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? type { get; set; } //1=personal info 2= address 3=Job History 4= Promotion Information 5=Educational Information 6=Training Information 7= Award Information 8=Medical History 9= Foreign Travel Information 10=Family Information
        
        public string reason { get; set; }

        public int? status { get; set; }

        public string remarks { get; set; }

        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
