using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Award:Base
    {
        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        public string awardName { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string awardNameBn { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string awardShortName { get; set; }
        public int? shortOrder { get; set; }
    }
}
