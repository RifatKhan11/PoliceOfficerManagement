using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.AddressData
{
    public class DistrictInfo:Base
    {
        public int? divisionId { get; set; }
        public DivisionInfo division { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string name { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string nameBn { get; set; }
        [Column(TypeName = "NVARCHAR(20)")]
        public string lat { get; set; }
        [Column(TypeName = "NVARCHAR(20)")]
        public string lon { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string url { get; set; }
    }
}
