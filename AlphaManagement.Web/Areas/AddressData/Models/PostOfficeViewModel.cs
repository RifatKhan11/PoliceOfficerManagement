using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class PostOfficeViewModel
    {
        public int PostOfficeId { get; set; }
        public int districtId { get; set; }
        public District district { get; set; }
        public int? thanaId { get; set; }
        public string postalCode { get; set; }

        public string postalName { get; set; }

        public string postalShortName { get; set; }

        public string postalNameBn { get; set; }
        public PostOfficeLn fLang { get; set; }
        public PostOffice postOffice { get; set; }
        public IEnumerable<PostOffice> postOfficeList { get; set; }
        public IEnumerable<District> districtList { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
    }
}
