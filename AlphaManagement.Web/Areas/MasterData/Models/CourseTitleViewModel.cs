using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class CourseTitleViewModel
    {
        public int CourseTitleId { get; set; }
        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }
        public CourseTitle courseTitle { get; set; }
        public CourseTitleLn fLang { get; set; }
        public IEnumerable<CourseTitle> courseTitles { get; set; }
    }
}
