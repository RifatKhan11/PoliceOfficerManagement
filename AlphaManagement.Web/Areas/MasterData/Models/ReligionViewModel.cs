using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class ReligionViewModel
    {
        public int ReligionId { get; set; }
        public string name { get; set; }
        public string nameBn { get; set; }
        public string shortName { get; set; }
        public ReligionLn fLang { get; set; }
        public Religion religion { get; set; }
        public IEnumerable<Religion> religions { get; set; }
    }
}
