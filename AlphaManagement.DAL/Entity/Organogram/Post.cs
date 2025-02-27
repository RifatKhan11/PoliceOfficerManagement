using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.Organogram
{
   public class Post:Base
    {
        public int? organoOrganizationId { get; set; }
        public OrganoOrganization organoOrganization { get; set; }

        public int? designationId { get; set; }
        public Designation designation { get; set; }

        public int? altDesignationId { get; set; }
        public Designation altDesignation { get; set; }

        public int IsHead { get; set; }

        public int numberOfPost { get; set; }
    }
}
