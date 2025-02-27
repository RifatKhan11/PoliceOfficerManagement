using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class ActivityStatusViewModel
    {
        public int ActivityStatusId { get; set; }
        public string statusName { get; set; }
        public string statusNameBn { get; set; }
        public string shortName { get; set; }
        public ActivityStatus activityStatus { get; set; }
        public ActivityStatusLn fLang { get; set; }
        public IEnumerable<ActivityStatus> activityStatusList { get; set; }
    }
}
