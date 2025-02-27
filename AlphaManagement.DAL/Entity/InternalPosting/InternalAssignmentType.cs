using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.InternalPosting
{
    public class InternalAssignmentType:Base
    {
        public string typeName { get; set; }
        public string typeNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
