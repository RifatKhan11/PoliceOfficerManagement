using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class TrainingInstitute:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string trainingInstituteName { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string trainingInstituteNameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string trainingInstituteShortName { get; set; }
        public int? shortOrder { get; set; }
    }
}
