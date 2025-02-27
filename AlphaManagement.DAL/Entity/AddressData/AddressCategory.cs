using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.AddressData
{
    public class AddressCategory : Base
    {
        [Column(TypeName = "NVARCHAR(120)")]
        public string name { get; set; }
    }
}
