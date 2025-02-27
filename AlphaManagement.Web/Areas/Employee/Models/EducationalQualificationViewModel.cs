using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EducationalQualificationViewModel
    {
        public int EducationalQualificationId { get; set; }
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public string institution { get; set; }
        public int? resultId { get; set; }
        public Result result { get; set; }
        public string majorGroup { get; set; }
        public string grade { get; set; }
        public int? passingYear { get; set; }
        public int? degreeId { get; set; }
        public Degree degree { get; set; }
        public int? organizationId { get; set; }
        public Organization organization { get; set; }
        public int? reldegreesubjectId { get; set; }
        public RelDegreeSubject reldegreesubject { get; set; }
        public EducationalQualificationLn fLang { get; set; }
        public EducationalQualification educationalQualification { get; set; }
        public IEnumerable<EducationalQualification> educationalQualifications { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<Result> results { get; set; }
        public IEnumerable<Degree> degrees { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<RelDegreeSubject> relDegreeSubjects { get; set; }
        public IEnumerable<Subject> subjects { get; set; }
    }
}
