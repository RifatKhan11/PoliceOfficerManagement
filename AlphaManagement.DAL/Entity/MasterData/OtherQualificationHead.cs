using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class OtherQualificationHead:Base
    {
        [Column(TypeName = "NVARCHAR(150)")]
        public string name { get; set; }
        public int? shortOrder { get; set; }
    }
}
