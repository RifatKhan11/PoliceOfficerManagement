using PoliceOfficerManagement.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Areas.MasterData.Models
{
    public class RankViewModel
    {
        public int Id { get; set; }
        public string rankCode { get; set; }
        public string rankName { get; set; }
        public string rankNameBN { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public int? forceCatId { get; set; }
        public IEnumerable<Rank> Ranks { get;set; }
    }

    public class InstitutionInfoViewModel
    {
        public int Id { get; set; }
        public int? type { get; set; }// 1=home,2=abroad 
        public string nameBn { get; set; } 
        public string nameEn { get; set; } 
        public string establishYear { get; set; } 
        public string placeInfo { get; set; } 
        public int? instituteType { get; set; }// educational=1,training=2
        public IEnumerable<InstitutionInfo> InstitutionInfos { get; set; }
    }
}
