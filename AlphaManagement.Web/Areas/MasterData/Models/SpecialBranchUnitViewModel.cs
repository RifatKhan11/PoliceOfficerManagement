using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class SpecialBranchUnitViewModel
    {
        public int SpecialBranchUnitId { get; set; }
        public string branchUnitName { get; set; }
        public string branchUnitNameBN { get; set; }
        public string branchCode { get; set; }
        public int? shortOrder { get; set; }
        public int? rankId { get; set; }
        public int? noOfPost { get; set; }
        public int? headUnitId { get; set; }
        public int? isparent { get; set; }
        public int? postInUnitId { get; set; }
        public int? SectionitId { get; set; }
        public int? isdefault { get; set; }
        public int? districtId { get; set; }
        public string NameBN { get; set; }
        public string Name { get; set; }
        public SpecialBranchUnitLn fLang { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
        public IEnumerable<PostInUnit> postInUnits { get; set; }
        public IEnumerable<Section>  sections { get; set; }
        public IEnumerable<District> districts { get; set; }
    }
}
