using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class NationalIdentityTypeViewModel
    {
        public int NationalIdentityTypeId { get; set; }
        public string nationalIdentityName { get; set; }
        public string nationalIdentityNameBn { get; set; }
        public int? shortOrder { get; set; }
        public NationalIdentityTypeLn fLang { get; set; }
        public NationalIdentityType nationalIdentityType { get; set; }
        public IEnumerable<NationalIdentityType> nationalIdentityTypeList { get; set; }
    }
}
