using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class TrainingInstituteViewModel
    {
        public int TrainingInstituteId { get; set; }
        public string trainingInstituteName { get; set; }
        public string trainingInstituteNameBn { get; set; }
        public string trainingInstituteShortName { get; set; }
        public int? shortOrder { get; set; }
        public TrainingInstituteLn fLang { get; set; }
        public TrainingInstitute trainingInstitute { get; set; }
        public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }
    }
}
