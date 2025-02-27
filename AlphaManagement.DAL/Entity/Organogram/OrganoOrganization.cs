using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.Organogram
{
   public class OrganoOrganization:Base
    {
        public int? organoOrganizationId { get; set; }
        public OrganoOrganization organoOrganization { get; set; }

        public int? organizationTypeId { get; set; }
        public OrganizationType organizationType { get; set; }

        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }
    }
}
