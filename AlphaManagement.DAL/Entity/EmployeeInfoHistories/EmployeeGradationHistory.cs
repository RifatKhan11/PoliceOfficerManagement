using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
    public class EmployeeGradationHistory:Base
    {
        public int? gradationId { get; set; }
        public EmployeeGradation gradation { get; set; }
        public int? gradationSerial { get; set; }
        public int? statusId { get; set; } //1=Active,2=Inactive
    }
}
