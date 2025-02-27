using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class MetropolitanAreaViewModel
    {
        public int MetropolitanAreaId { get; set; }
        public string areaName { get; set; }
        public string areaNameBn { get; set; }
        public int? districtId { get; set; }
        public int? shortOrder { get; set; }
        public MetropolitanAreaLn fLang { get; set; }
        public MetropolitanArea metropolitanArea { get; set; }
        public IEnumerable<MetropolitanArea> metropolitanAreaList { get; set; }
        public IEnumerable<District> DistrictList { get; set; }
    }
}
