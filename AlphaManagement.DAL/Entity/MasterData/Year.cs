using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Year : Base
    {
        [Column(TypeName = "nvarchar(10)")]
        public string year { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string remarks { get; set; }
    }
}
