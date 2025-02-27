using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class AddressCategoryViewModel
    {
        public int AddressCategoryId { get; set; }
        public string name { get; set; }
        public AddressCategory addressCategory { get; set; }
        public AddressCategoryLn fLang { get; set; }
        public IEnumerable<AddressCategory> addressCategoryList { get; set; }

    }
}
