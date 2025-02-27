using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class RelDegreeSubjectViewModel
    {
        public int RelDegreeSubjectId { get; set; }
        public int degreeId { get; set; }
        public Degree degree { get; set; }
        public int subjectId { get; set; }
        public RelDegreeSubjectLn fLang { get; set; }
        public Subject subject { get; set; }
        public RelDegreeSubject relDegreeSubject { get; set; }
        public IEnumerable<RelDegreeSubject> relDegreeSubjects { get; set; }
        public IEnumerable<Degree> degreeList { get; set; }
        public IEnumerable<Subject> subjects { get; set; }

        
    }
}
