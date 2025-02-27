using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class DistrictViewModel
    {
        public int DistrictId { get; set; }
        public int divisionId { get; set; }
        public Division division { get; set; }

        public string districtCode { get; set; }
        public string districtName { get; set; }

        public string districtNameBn { get; set; }

        public string shortName { get; set; }

        public string isActive { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public DistrictLn fLang { get; set; }
        public District district { get; set; }
        public IEnumerable<District> districtList { get; set; }
        public IEnumerable<Division> divisionList { get; set; }
    }
}
