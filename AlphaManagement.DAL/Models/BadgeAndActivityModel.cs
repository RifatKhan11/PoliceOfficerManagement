using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models
{
    public  class BadgeAndActivityModel
    {
        public int? employeeId { get; set; }
        public string employeeCode { get; set; }
        public string colorCode { get; set; }
        public string attachedUnit { get; set; }
        public string expertise { get; set; }
        public string skillIcon { get; set; }
        public int? lockedEmpId { get; set; }
        public int? pHQTRTypeId { get; set; }
    }
}
