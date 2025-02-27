using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Religion:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string name { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string nameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string shortName { get; set; }
    }
}
