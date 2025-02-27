using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class VillageViewModel
    {
        public int VillageId { get; set; }
        public int unionWardId { get; set; }
        public UnionWard unionWard { get; set; }
        public int? thanaId { get; set; }
        public Thana thana { get; set; }
        public int? districtsId { get; set; }
        public District districts { get; set; }
        public string villageCode { get; set; }
        public string villageName { get; set; }
        public string villageNameBn { get; set; }
        public string shortName { get; set; }
        public string isActive { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public VillageLn fLang { get; set; }
        public Village Village { get; set; }
        public IEnumerable<Village> villageList { get; set; }
        public IEnumerable<UnionWard> unionWardList { get; set; }
        public IEnumerable<Thana> thanaList { get; set; }
        public IEnumerable<District> districtList { get; set; }
    }
}
