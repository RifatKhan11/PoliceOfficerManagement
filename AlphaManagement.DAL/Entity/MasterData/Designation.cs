using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Designation:Base
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string designationCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string designationName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string designationNameBN { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        //public int? empType { get; set; }
    }
}
