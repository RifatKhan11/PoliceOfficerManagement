using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models.Lang
{
    public class CountryLn
    {
        public string Title { get; set; }
        public string countryCode { get; set; }

        public string countryName { get; set; }

        public string countryNameBn { get; set; }

        public string shortName { get; set; }

        public string isActive { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public string action { get; set; }
    }
}
