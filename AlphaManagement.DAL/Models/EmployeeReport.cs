using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EmployeeReport
    {
        public string employeeCode { get; set; }
        public int? employeeId { get; set; }
        public string nameEnglish { get; set; }
        public string unit { get; set; }
        public string rank { get; set; }
        public string bcsBatch { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string nid { get; set; }
        public string dateOfbirth { get; set; }
        public string officeMobile { get; set; }
        public string personalMobile { get; set; }
        public string emailOffice { get; set; }
        public string emailPersona { get; set; }
        public string imageUrl { get; set; }
        public string meritePosition { get; set; }
        public string homeDistrict { get; set; }
        public List<ItemDetails> itemDetails { get; set; }

        //Gradation Info
        public string joiningRank { get; set; }
        public string joiningDate { get; set; }
        public string seniorRankDate { get; set; }
        public string lastPromotionDate { get; set; }
        public string educationQualification { get; set; }
        public int? gradationNo { get; set; }
    }

    public class ItemDetails
    {
        public string colName { get; set; }
        public string colValue { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
    public class EmployeeSearchReport
    {
        public string employeeCode { get; set; }
        public string BPNumberBn { get; set; }
        public int employeeId { get; set; }
        public string nameEnglish { get; set; }
        public string nameBangla { get; set; }
        public string unit { get; set; }
        public string rank { get; set; }
        public string bcsBatch { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string nid { get; set; }
        public string dateOfbirth { get; set; }
        public string joiningDateGovtService { get; set; }
        public string officeMobile { get; set; }
        public string personalMobile { get; set; }
        public string emailOffice { get; set; }
        public string emailPersona { get; set; }
        public string imageUrl { get; set; }
        public string meritePosition { get; set; }
        public string homeDistrict { get; set; }
        public string joiningDesignation { get; set; }
        public string promotionJoiningDate { get; set; }
        public string currentPositionJoiningDate { get; set; }
        public string educationalQualifaction { get; set; }
        public string currentRankNameBn { get; set; }
        public string seniorScalePromotionDate { get; set; }
        public string birthPlace { get; set; }
        public IEnumerable<EmployeeSearchReport> employeeInfoReports { get; set; }
    }
}
