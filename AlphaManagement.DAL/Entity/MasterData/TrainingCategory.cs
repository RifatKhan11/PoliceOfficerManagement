using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class TrainingCategory:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string trainingCategoryName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string trainingCategoryNameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string trainingCategoryShortName { get; set; }
        public int? shortOrder { get; set; }
    }
}
