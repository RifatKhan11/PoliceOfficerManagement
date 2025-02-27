using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EmployeeProfileModel
    {
        public IEnumerable<Spouse> spouses { get; set; }
        public IEnumerable<AwardEntry> awardEntries { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public IEnumerable<Assignment> assignments { get; set; }
        public IEnumerable<EducationalQualification> educationalQualifications { get; set; }
        public IEnumerable<AddressInformation> addressInformation { get; set; }
        public IEnumerable<PromotionLog> promotionLogs { get; set; }
        public IEnumerable<TraningLog> traningLogs { get; set; }
        public IEnumerable<DisciplinaryAction> disciplinaryActions { get; set; }
        public Photograph photograph { get; set; }
    }
}
