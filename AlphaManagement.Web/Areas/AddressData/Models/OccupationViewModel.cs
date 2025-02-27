using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class OccupationViewModel
    {
        public int OccupationId { get; set; }
        public string name { get; set; }

        public string nameBn { get; set; }

        public string imagePath { get; set; }
        public string shortOrder { get; set; }
        public OccupationLn fLang { get; set; }
        public Occupation occupation { get; set; }
        public IEnumerable<Occupation> occupationList { get; set; }
    }
}
