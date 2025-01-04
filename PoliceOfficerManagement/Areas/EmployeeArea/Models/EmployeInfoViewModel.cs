using PoliceOfficerManagement.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Areas.EmployeeArea.Models
{
    public class EmployeInfoViewModel
    { 
        public int Id { get; set; }
        public string nameEn { get; set; } 
        public string nameBn { get; set; } 
        public string employeeCode { get; set; }
        public string nidNumber { get; set; }
        public DateTime? joiningDate { get; set; }
        public DateTime? retirementDate { get; set; }
        public int? joiningRankId { get; set; } 
        public string homeDistrict { get; set; } 
        public string homeUpazila { get; set; } 
        public string reMarks { get; set; } 
        public string personalPhoneNumber { get; set; } 
        public string officePhoneNumber { get; set; }

        public IEnumerable<EmployeInfo> employeInfos { get; set; }
        public IEnumerable<InstitutionInfo> institutionInfos { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<Rank> Ranks { get; set; }




        #region Educational Info
        public int[] eInstituteId { get; set; }
        public string[] rPassingYear { get; set; }
        public string[] eBatchNo { get; set; }
        public string[] eGrade { get; set; }
        public string[] eDegreeName { get; set; }
        public string[] eAchievement { get; set; }
        public string[] eCourseDuration { get; set; }
        #endregion

        #region Institute Info
        public int[] instituteId { get; set; }        
        public string[] passingYear { get; set; }       
        public string[] batchNo { get; set; }
        public string[] grade { get; set; }
        public string[] degreeName { get; set; }
        public string[] achievement { get; set; }
        public string[] courseDuration { get; set; }

        #endregion

        #region Posting
        public int[] rankId { get; set; }
        public DateTime[] postingFrom { get; set; }
        public DateTime[] postingTo { get; set; }

        public int[] thanaId { get; set; }
        public int[] zoneId { get; set; }
        public int[] districtId { get; set; }
        public int[] rangeId { get; set; }
        public string[] pReMarks { get; set; }
        public DateTime[] promotionDate { get; set; }
        #endregion

        #region Address Info
        public int[] addressType { get; set; }// 1=PRESENT, 2=PERMANENT
        public string[] roadInfo { get; set; }
        public int[] aVillegeId { get; set; }
        public int[] aUnionId { get; set; }
        public int[] aThanaId { get; set; }
        public int[] aDistrictId { get; set; }
        public int[] aDivisionId { get; set; }
        #endregion
    }
}
