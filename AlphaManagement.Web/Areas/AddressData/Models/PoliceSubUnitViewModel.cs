using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class PoliceSubUnitViewModel
    {
        public int PoliceSubUnitId { get; set; }
        public PoliceUnit policeUnit { get; set; }

        public string subunitName { get; set; }

        public string subunitNameBn { get; set; }

        public string isActive { get; set; }

        public string isReportable { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public PoliceSubUnitLn fLang { get; set; }
        public PoliceSubUnit policeSubUnit { get; set; }
        public IEnumerable<PoliceSubUnit> policeSubUnitList { get; set; }
        public IEnumerable<PoliceUnit> policeUnitList { get; set; }
    }
}
