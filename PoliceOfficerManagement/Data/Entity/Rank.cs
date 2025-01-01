using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Data.Entity
{
    public class Rank:Base
    {
        [StringLength(100)]
        public string rankCode { get; set; }
        [StringLength(400)]
        public string rankName { get; set; }
        [StringLength(400)]
        public string rankNameBN { get; set; }
        [StringLength(100)]
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public int? forceCatId { get; set; }
    }
}
