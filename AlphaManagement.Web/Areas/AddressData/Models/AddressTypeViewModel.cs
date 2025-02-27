using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class AddressTypeViewModel
    {
        public int AddressTypeId { get; set; }
        public string typeName { get; set; }
        public AddressTypeLn fLang { get; set; }
        public AddressType addressType { get; set; }
        public IEnumerable<AddressType> addressTypeList { get; set; }
    }
}
