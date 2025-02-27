using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.AddressData
{
    public class PoliceSubUnit : Base
    {
        public int? policeUnitId { get; set; }
        public PoliceUnit policeUnit { get; set; }       

        [Column(TypeName = "NVARCHAR(350)")]
        public string subunitName { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string subunitNameBn { get; set; }        
        [Column(TypeName = "NVARCHAR(10)")]
        public string isActive { get; set; }
        [Column(TypeName = "NVARCHAR(10)")]
        public string isReportable { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string latitude { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string longitude { get; set; }
    }
}
