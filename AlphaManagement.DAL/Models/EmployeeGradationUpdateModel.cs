using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models
{
    public class EmployeeGradationUpdateModel
    {
        public int? gradationId { get; set; }
        public List<int?> lstGradationId { get; set; }
    }
}
