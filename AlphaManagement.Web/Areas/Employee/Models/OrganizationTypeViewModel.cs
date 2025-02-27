using AlphaManagement.DAL.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class OrganizationTypeViewModel
    {
        public int organizationTypeId { get; set; }

        [Required]
        [Display(Name = "Organization Type Name")]
        public string organizationTypeName { get; set; }

        public string organizationTypeNameBN { get; set; }

        public string remarks { get; set; }
        
        public IEnumerable<OrganizationType> organizationTypes { get; set; }
    }
}
