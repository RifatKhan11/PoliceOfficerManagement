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
    public class DisciplinaryActionViewModel
    {
        public int DisciplinaryActionId { get; set; }
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public int? OffenseId { get; set; }
        public Offense Offense { get; set; }
        public int? naturalPunishmentId { get; set; }
        public NaturalPunishment naturalPunishment { get; set; }
        public DateTime? punishmentDate { get; set; }
        public DateTime? startingDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime? goNumberWithDate { get; set; }
        public string goFileURL { get; set; }
        public string remarks { get; set; }


        public string OffenseName { get; set; }
        public string PunishmentName { get; set; }
        public string referenceNumber { get; set; }
        public string status { get; set; }
        public string type { get; set; }

        public int acrId { get; set; }
        public int? year { get; set; }
        public decimal? marks { get; set; }


        public IEnumerable<ForeignTravel> foreignTravels { get; set; }
        public DisciplinaryActionLn fLang { get; set; }
        public DisciplinaryAction disciplinaryAction { get; set; }
        public IEnumerable<DisciplinaryAction> disciplinaryActions { get; set; }
        public IEnumerable<ACRInformation> acrInformations { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public IEnumerable<Offense> offenses { get; set; }
        public IEnumerable<NaturalPunishment> naturalPunishments { get; set; }
        public IEnumerable<TraningLog> traningLogs { get; set; }
        public IEnumerable<TrainingCategory> trainingCategories { get; set; }
        public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }
        public IEnumerable<Country> countries { get; set; }
        public IEnumerable<SpecialSkillType> specialSkillTypes { get; set; }
        public IEnumerable<EmployeeReportInfo> employeeReportInfos { get; set; }
        public IEnumerable<EmployeeMadicalInfo> employeeMadicalInfos { get; set; }
        public IEnumerable<MedicalMainCategory> medicalMainCategories { get; set; }
        public IEnumerable<MedicalSubCategory> medicalSubCategories { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
        public IEnumerable<BCSBatch> bCSBatches { get; set; }
    }
}
