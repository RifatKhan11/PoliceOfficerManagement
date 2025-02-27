using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Relation:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string relationName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string relationNameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string relationShortName { get; set; }
    }
}
