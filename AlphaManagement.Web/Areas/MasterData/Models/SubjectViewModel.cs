using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class SubjectViewModel
    {
        public int SubjectId { get; set; }
        public string subjectName { get; set; }
        public string subjectNameBn { get; set; }
        public string subjectShortName { get; set; }
        public int? shortOrder { get; set; }
        public SubjectLn fLang { get; set; }
        public Subject subject { get; set; }
        public IEnumerable<Subject> subjects { get; set; }
    }
}
