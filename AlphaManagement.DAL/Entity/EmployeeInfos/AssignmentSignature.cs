using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class AssignmentSignature:Base
    {
        public int? igpSignId { get; set; }
        public EmployeeInfo igpSign { get; set; }
        public int? addlIGPSignId { get; set; }
        public EmployeeInfo addlIGPSign { get; set; }
        public int? digSignId { get; set; }
        public EmployeeInfo digSign { get; set; }
        public int? addlDIGSignId { get; set; }
        public EmployeeInfo addlDIGSign { get; set; }
        public int? spSignId { get; set; }
        public EmployeeInfo spSign { get; set; }
        public int? addlSPSignId { get; set; }
        public EmployeeInfo addlSPSign { get; set; }
        public int? aspSignSignId { get; set; }
        public EmployeeInfo aspSign { get; set; }
        public int? inspectorSignId { get; set; }
        public EmployeeInfo inspectorSign { get; set; }
        public int? typeId { get; set; }
    }
}
