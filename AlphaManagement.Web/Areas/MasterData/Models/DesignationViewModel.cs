using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class DesignationViewModel
    {
        public int DesignationId { get; set; }
        public string designationCode { get; set; }
        public string designationName { get; set; }
        public string designationNameBN { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public DesignationLn fLang { get; set; }
        public Designation designation { get; set; }
        public IEnumerable<Designation> designationList { get; set; }
    }
}
