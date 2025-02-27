using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class RankViewModel
    {
        public int RankId { get; set; }
        public string rankCode { get; set; }
        public string rankName { get; set; }
        public string rankNameBN { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public RankLn fLang { get; set; }
        public Rank Rank { get; set; }
        public IEnumerable<Rank> Ranks { get; set; }
    }
}
