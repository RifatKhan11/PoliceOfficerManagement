using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class UnionWardViewModel
    {
        public int? unionWardId { get; set; }
        public int? thanaId { get; set; }
        public int? districtId { get; set; }
        public string unionCode { get; set; }
        public string nameEnglish { get; set; }
        public string nameBangla { get; set; }
        public string isActive { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public string unionName { get; set; }

        public string unionNameBn { get; set; }

        public string shortName { get; set; }
        public UnionWardLn fLang { get; set; }
        public UnionWard unionWard { get; set; }
        public IEnumerable<UnionWard> unionWardList { get; set; }
        public IEnumerable<Thana> thanaList { get; set; }
        public IEnumerable<District> districtList { get; set; }
        public IEnumerable<Division> divisions { get; set; }
    }
}
