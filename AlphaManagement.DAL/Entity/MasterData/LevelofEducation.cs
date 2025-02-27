using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class LevelofEducation:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string levelofeducationName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string levelofeducationNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
