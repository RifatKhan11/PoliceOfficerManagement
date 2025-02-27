using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.AddressData
{
    public class AddressType : Base
    {
        [Required]
        [Column(TypeName = "NVARCHAR(120)")]
        public string typeName { get; set; }
    }
}
