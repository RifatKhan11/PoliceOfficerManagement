using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class DivisionViewModel
    {
        public int DivisionId { get; set; }
        public int? countryId { get; set; }
        public Country country { get; set; }

        public string divisionCode { get; set; }

        public string divisionName { get; set; }
        public string divisionNameBn { get; set; }

        public string shortName { get; set; }

        public string isActive { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public DivisionLn fLang { get; set; }
        public Division Division { get; set; }
        public IEnumerable<Division> divisionList { get; set; }
        public IEnumerable<Country> countryList { get; set; }
    }
}
