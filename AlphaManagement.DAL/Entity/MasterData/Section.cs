using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Section:Base
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string NameBN { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }
        public int? rankId { get; set; }
        public Rank rank { get; set; }
    }
}
