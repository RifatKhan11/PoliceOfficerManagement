using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class OrganoOrganizationViewModel
    {
        public int organoOrganizationId { get; set; }

        public int? organizationTypeId { get; set; }

        public int? organoOrganizationParrentId { get; set; }

        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }

        public int designationId { get; set; }

        public int? altDesignationId { get; set; }

        public int numberOfPost { get; set; }

        public int IsHead { get; set; }
        

        public IEnumerable<Designation> designations { get; set; }

        public IEnumerable<OrganoOrganization> organoOrganizations { get; set; }

        public IEnumerable<OrganizationType> organizationTypes { get; set; }

        public IEnumerable<OrganoOrganization> organoRoots { get; set; }

        public string organizationType { get; set; }
    }
}
