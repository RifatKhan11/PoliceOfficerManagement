using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class OrganizationViewModel
    {
      
        public int orgId { get; set; }
        public string organizationName { get; set; }
        public string organizationNameBn { get; set; }
        public string organizationType { get; set; }
        public int? shortOrder { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
    }
}
