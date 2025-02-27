using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Result : Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string resultName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string resultNameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string resultShortName { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal resultMaxValue { get; set; }
    }
}
