using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class DivisionDistrictViewModel
    {
        public int DivisionDistrictId { get; set; }
        public int? rangeMetroId { get; set; }
        public RangeMetro rangeMetro { get; set; }

        public string divisionDistrictName { get; set; }

        public string divisionDistrictNameBn { get; set; }

        public string isActive { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public DivisionDistrictLn fLang { get; set; }
        public DivisionDistrict divisionDistrict { get; set; }
        public IEnumerable<DivisionDistrict> divisionDistrictList { get; set; }
        public IEnumerable<RangeMetro> rangeMetroList { get; set; }
    }
}
