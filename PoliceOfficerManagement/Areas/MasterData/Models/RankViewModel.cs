using PoliceOfficerManagement.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool IsActive { get; set; }

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
        public string instituteType { get; set; }// educational=1,training=2
        public bool IsActive { get; set; }

        public IEnumerable<InstitutionInfo> InstitutionInfos { get; set; }
    }

    public class RangeMetrosViewModel
    {
        public int Id { get; set; }
        public string rangeMetroName { get; set; } 
        public string rangeMetroNameBn { get; set; }
        public bool IsActive { get; set; }
        public string latitude { get; set; } 
        public string longitude { get; set; } 
        public int? pimsRangeId { get; set; }
        public string pimsRangeName { get; set; }

        public IEnumerable<RangeMetro> Ranges { get; set; }
    }
    public class ZoneCircleViewModel
    {
        public int Id { get; set; }
        public int? divisionDistrictId { get; set; } 
        public string zoneName { get; set; } 
        public string zoneNameBn { get; set; } 
        public string latitude { get; set; } 
        public string longitude { get; set; } 
        public int? pimsZoneId { get; set; }
        public string pimsZoneName { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<ZoneCircle> zones { get; set; }
        public IEnumerable<DivisionDistrict> divisionDistricts { get; set; }

    }

    public class DivisionDistrictViewModel
    {
        public int Id { get; set; }
        public int? rangeMetroId { get; set; }  
        public string divisionDistrictName { get; set; } 
        public string divisionDistrictNameBn { get; set; } 
        public string latitude { get; set; } 
        public string longitude { get; set; } 
        public int? pimsDistrictId { get; set; }
        public string pimsDistrictName { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<DivisionDistrict> divisionDistricts { get; set; }
        public IEnumerable<RangeMetro> Ranges { get; set; }
    }

    public class PoliceThanaViewModel
    {
        public int Id { get; set; }
        public int? rangeMetroId { get; set; } 
        public int? divisionDistrictId { get; set; } 
        public int? zoneCircleId { get; set; } 
        public int? upazillaId { get; set; } 
        public string policeThanaName { get; set; } 
        public string policeThanaNameBn { get; set; } 
        public string isReportable { get; set; } 
        public string latitude { get; set; } 
        public string longitude { get; set; } 
        public int? policeThanaId { get; set; }  
        public string address { get; set; }
        public int? fariType { get; set; } // 1=fari, 2=camp,3=todonto kendro
        public int? isChild { get; set; }
        public int? pimsThanaId { get; set; }
        public string pimsThanaName { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<RangeMetro> Ranges { get; set; }
        public IEnumerable<DivisionDistrict> divisionDistricts { get; set; }
        public IEnumerable<ZoneCircle> zone { get; set; }
        public IEnumerable<Thana> upazilla { get; set; }
        public IEnumerable<PoliceThana> policeThanas { get; set; } 
     }
}
