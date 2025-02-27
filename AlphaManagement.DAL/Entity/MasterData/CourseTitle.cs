using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class CourseTitle : Base
    {
        [Column(TypeName = "nvarchar(200)")]
        public string nameEN { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string nameBN { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string remarks { get; set; }
    }
}
