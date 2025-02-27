using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Language:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string languageName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string languageNameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string languageShortName { get; set; }
        public int? shortOrder { get; set; }
    }
}
