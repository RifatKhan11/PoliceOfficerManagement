using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class PoliceUnitViewModel
    {
        public int PoliceUnitId { get; set; }
        public int? rangeMetroId { get; set; }
        public RangeMetro rangeMetro { get; set; }

        public int? policeThanaId { get; set; }
        public PoliceThana policeThana { get; set; }

        public string unitName { get; set; }

        public string unitNameBn { get; set; }

        public string isActive { get; set; }

        public string isReportable { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public PoliceUnitLn fLang { get; set; }
        public PoliceUnit PoliceUnit { get; set; }
        public IEnumerable<PoliceUnit> policeUnitList { get; set; }
        public IEnumerable<RangeMetro> rangeMetroList { get; set; }
        public IEnumerable<PoliceThana> policeThanaList { get; set; }
    }
}
