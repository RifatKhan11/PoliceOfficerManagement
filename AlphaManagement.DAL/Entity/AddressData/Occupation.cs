using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.AddressData
{
    public class Occupation:Base
    {
        [Column(TypeName = "NVARCHAR(120)")]
        public string name { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string nameBn { get; set; }
        [Column(TypeName = "NVARCHAR(420)")]
        public string imagePath { get; set; }
        public string shortOrder { get; set; }
    }
}
