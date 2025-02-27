using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models.Lang
{
    public class DistrictLn
    {

        public string Title { get; set; }
        public string District { get; set; }
        public string division { get; set; }

        public string districtCode { get; set; }
        public string districtName { get; set; }

        public string districtNameBn { get; set; }

        public string shortName { get; set; }

        public string isActive { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public string action { get; set; }
    }
}
