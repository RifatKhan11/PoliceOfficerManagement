using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class QualificationHead:Base
    {
        [Column(TypeName = "NVARCHAR(250)")]
        public string name { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string nameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
