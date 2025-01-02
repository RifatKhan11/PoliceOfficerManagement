using PoliceOfficerManagement.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Areas.MasterData.Models
{
    public class RankViewModel
    {
        public int Id { get; set; }
        public string rankCode { get; set; }
        public string rankName { get; set; }
        public string rankNameBN { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public int? forceCatId { get; set; }
        public IEnumerable<Rank> Ranks { get;set; }
    }
}
