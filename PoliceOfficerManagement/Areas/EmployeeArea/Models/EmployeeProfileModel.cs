using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Areas.EmployeeArea.Models
{
    public class EmployeeProfileModel
    {
        public IEnumerable<TrainingInfo> TrainingInfo { get; set; }
        public IEnumerable<EducationalInfo> EducationalInfo { get; set; }
        public IEnumerable<AdderssInfo> AdderssInfo { get; set; }
        public IEnumerable<PostingPlace> PostingPlace { get; set; }
        public EmployeeInfoModel EmployeeInfo { get; set; }
    }
}
