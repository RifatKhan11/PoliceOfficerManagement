using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class ActivityStatus:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string statusName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string statusNameBn { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string shortName { get; set; }
    }
}
