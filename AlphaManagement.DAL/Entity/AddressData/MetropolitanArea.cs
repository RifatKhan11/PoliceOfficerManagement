using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.AddressData
{
    public class MetropolitanArea:Base
    {
        [Column(TypeName = "NVARCHAR(150)")]
        public string areaName { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string areaNameBn { get; set; }
        public int? districtId { get; set; }
        public int? shortOrder { get; set; }
    }
}
