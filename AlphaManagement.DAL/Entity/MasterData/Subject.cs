using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Subject:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string subjectName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string subjectNameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string subjectShortName { get; set; }
        public int? shortOrder { get; set; }
    }
}
