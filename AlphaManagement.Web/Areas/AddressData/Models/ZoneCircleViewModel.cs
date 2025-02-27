using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class ZoneCircleViewModel
    {
        public int ZoneCircleId { get; set; }
        public int? divisionDistrictId { get; set; }
        public DivisionDistrict divisionDistrict { get; set; }
        public string zoneName { get; set; }
        public string zoneNameBn { get; set; }
        public string isActive { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public ZoneCircleLn fLang { get; set; }
        public ZoneCircle zoneCircle { get; set; }
        public IEnumerable<ZoneCircle> zoneCircleList { get; set; }
        public IEnumerable<DivisionDistrict> divisionDistrictList { get; set; }


    }
}
