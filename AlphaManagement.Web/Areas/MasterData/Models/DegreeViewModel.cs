using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class DegreeViewModel
    {
        public int DegreeId { get; set; }
        public string degreeName { get; set; }
        public string degreeNameBn { get; set; }
        public string degreeShortName { get; set; }
        public int levelofeducationId { get; set; }
        public int? shortOrder { get; set; }
        public Degree degree { get; set; }
        public DegreeLn fLang { get; set; }
        public IEnumerable<Degree> degreeList { get; set; }
        public IEnumerable<LevelofEducation> LevelofEducations { get; set; }
    }
}
