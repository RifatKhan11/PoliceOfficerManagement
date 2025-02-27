using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class ThanaViewModel
    {
        public int ThanaId { get; set; }
        public int? districtId { get; set; }
        public District district { get; set; }

        public int? rangeMetroId { get; set; }
        public RangeMetro rangeMetro { get; set; }

        public string thanaCode { get; set; }

        public string thanaName { get; set; }

        public string thanaNameBn { get; set; }

        public string shortName { get; set; }

        public string isActive { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public ThanaLn fLang { get; set; }
        public Thana thana { get; set; }
        public IEnumerable<Thana> thanaList { get; set; }
        public IEnumerable<District> districtList { get; set; }
        public IEnumerable<RangeMetro> rangeMetroList { get; set; }
    }
}
