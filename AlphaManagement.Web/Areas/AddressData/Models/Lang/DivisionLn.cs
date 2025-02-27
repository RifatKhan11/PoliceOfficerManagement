using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models.Lang
{
    public class DivisionLn
    {
        public string Title { get; set; }
        public string Division { get; set; }
        public string country { get; set; }

        public string divisionCode { get; set; }

        public string divisionName { get; set; }
        public string divisionNameBn { get; set; }

        public string shortName { get; set; }

        public string isActive { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
        public string action { get; set; }
    }
}
