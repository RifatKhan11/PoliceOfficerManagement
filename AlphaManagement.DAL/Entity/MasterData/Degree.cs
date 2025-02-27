using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Degree : Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string degreeName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string degreeNameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string degreeShortName { get; set; }

        public int levelofeducationId { get; set; }
        public LevelofEducation levelofeducation { get; set; }
    }
}
