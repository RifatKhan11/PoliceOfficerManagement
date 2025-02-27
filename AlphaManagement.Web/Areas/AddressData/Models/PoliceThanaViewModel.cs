using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class PoliceThanaViewModel
    {
        public int PoliceThanaId { get; set; }
        public int? rangeMetroId { get; set; }
        public RangeMetro rangeMetro { get; set; }

        public int? divisionDistrictId { get; set; }
        public DivisionDistrict divisionDistrict { get; set; }

        public int? zoneCircleId { get; set; }
        public ZoneCircle zoneCircle { get; set; }

        public int? upazillaId { get; set; }
        public Thana upazilla { get; set; }

    
        public string policeThanaName { get; set; }

        public string policeThanaNameBn { get; set; }
    
        public string isActive { get; set; }
     
        public string isReportable { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public PoliceThanaLn fLang { get; set; }
        public PoliceThana policeThana { get; set; }
        public IEnumerable<PoliceThana> policeThanaList { get; set; }
        public IEnumerable<RangeMetro> rangeMetroList { get; set; }
        public IEnumerable<DivisionDistrict> divisionDistrictList { get; set; }
        public IEnumerable<ZoneCircle> zoneCircleList { get; set; }
        public IEnumerable<Thana> thanaList { get; set; }
    }
}
