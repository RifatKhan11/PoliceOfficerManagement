using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOfficerManagement.Data.Entity
{
    public class ZoneCircle:Base
    {
        public int? divisionDistrictId { get; set; }
        public DivisionDistrict divisionDistrict { get; set; }

        [Column(TypeName = "NVARCHAR(350)")]
        public string zoneName { get; set; }

        [Column(TypeName = "NVARCHAR(350)")]
        public string zoneNameBn { get; set; }
       
        [Column(TypeName = "NVARCHAR(120)")]
        public string latitude { get; set; }

        [Column(TypeName = "NVARCHAR(120)")]
        public string longitude { get; set; }

        public int? pimsZoneId { get; set; }
        public string pimsZoneName { get; set; }
        
    }
}
