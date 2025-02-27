using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class TraningLogViewModel
    {
        public int TraningLogId { get; set; }
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public int? countryId { get; set; }
        public Country country { get; set; }
        public int? trainingCategoryId { get; set; }
        public TrainingCategory trainingCategory { get; set; }
        public int? trainingInstituteId { get; set; }
        public TrainingInstitute trainingInstitute { get; set; }
        public string remarks { get; set; }
        public string trainingTitle { get; set; }
        public string sponsoringAgency { get; set; }
        public TraningLogLn fLang { get; set; }
        public TraningLog traningLog { get; set; }
        public IEnumerable<TraningLog> traningLogs { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<Country> countries { get; set; }
        public IEnumerable<TrainingCategory> trainingCategories { get; set; }
        public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }

    }
}
