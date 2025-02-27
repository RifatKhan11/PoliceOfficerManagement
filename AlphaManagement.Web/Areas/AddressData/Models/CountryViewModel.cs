using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class CountryViewModel
    {
        public int CountryId { get; set; }
        public string countryCode { get; set; }

        public string countryName { get; set; }

        public string countryNameBn { get; set; }

        public string shortName { get; set; }

        public string isActive { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public CountryLn fLang { get; set; }
        public Country country { get; set; }
        public IEnumerable<Country> countryList { get; set; }
    }
}
