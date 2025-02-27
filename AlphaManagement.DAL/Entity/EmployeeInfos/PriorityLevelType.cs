using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
   public class PriorityLevelType : Base
    {
        public string name { get; set; }
        public string nameBn { get; set; }
        public int? sortOrder { get; set; }
    }
}
