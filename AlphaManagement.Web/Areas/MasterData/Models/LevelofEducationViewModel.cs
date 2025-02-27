using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class LevelofEducationViewModel
    {
        public int LevelofEducationId { get; set; }
        public string levelofeducationName { get; set; }
        public string levelofeducationNameBn { get; set; }
        public int? shortOrder { get; set; }
        public LevelofEducationLn fLang { get; set; }
        public LevelofEducation levelofEducation { get; set; }
        public IEnumerable<LevelofEducation> levelofEducations { get; set; }
    }
}
