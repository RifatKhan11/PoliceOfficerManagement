using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class AwardViewModel
    {
        public int AwardId { get; set; }
        public string awardName { get; set; }
        public string awardNameBn { get; set; }
        public string awardShortName { get; set; }
        public int? shortOrder { get; set; }

        public Award award { get; set; }
        public IEnumerable<Award> awards { get; set; }
    }
}
