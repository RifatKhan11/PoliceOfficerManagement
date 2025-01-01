using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOfficerManagement.Data.Entity
{
    public class Country:Base
    {
        [Column(TypeName = "NVARCHAR(20)")]
        public string countryCode { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string countryName { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string countryNameBn { get; set; }
        public string nationality { get; set; }
        [Column(TypeName = "NVARCHAR(20)")]
        public string shortName { get; set; }
        
        [Column(TypeName = "NVARCHAR(120)")]
        public string latitude { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string longitude { get; set; }
    }
}
