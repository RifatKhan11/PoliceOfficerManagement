using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class TrainingCategoryViewModel
    {
        public int TrainingCategoryId { get; set; }
        public string trainingCategoryName { get; set; }
        public string trainingCategoryNameBn { get; set; }
        public string trainingCategoryShortName { get; set; }
        public int? shortOrder { get; set; }
        public TrainingCategoryLn fLang { get; set; }
        public TrainingCategory trainingCategory { get; set; }
        public IEnumerable<TrainingCategory> trainingCategories { get; set; }
    }
}
